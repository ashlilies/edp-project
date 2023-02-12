using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using Amazon.TranscribeService;
using Amazon.TranscribeService.Model;
using GrowGreenWeb.Helpers;
using GrowGreenWeb.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GrowGreenWeb.Services;

public class TranscriptionService
{
    private readonly AmazonS3Client _s3Client;
    private readonly AmazonTranscribeServiceClient _transcribeClient;
    private readonly ILogger<TranscriptionService> _logger;
    private readonly IWebHostEnvironment _hostEnvironment;
    // private readonly GrowGreenContext db;
    private readonly IDbContextFactory<GrowGreenContext> _contextFactory;

    private readonly string _bucketName =
        Environment.GetEnvironmentVariable("VIONA_AWS_BUCKET")!;

    public TranscriptionService(
        ILogger<TranscriptionService> logger,
        IWebHostEnvironment hostEnvironment,
        IDbContextFactory<GrowGreenContext> contextFactory)
    {
        _logger = logger;
        _hostEnvironment = hostEnvironment;
        _contextFactory = contextFactory;


        string accessKey =
            Environment.GetEnvironmentVariable("AWS_REKOGNITION_ACCESS_KEY")!;
        string secretKey =
            Environment.GetEnvironmentVariable("AWS_REKOGNITION_SECRET_KEY")!;
        string region = Environment.GetEnvironmentVariable("AWS_REGION")!;

        var credentials =
            new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);
        var regionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region);

        _transcribeClient =
            new AmazonTranscribeServiceClient(credentials, regionEndpoint);
        _s3Client =
            new AmazonS3Client(credentials, regionEndpoint);
    }

    /// <summary>
    /// Convert a video file to MP3 and transcribes it
    /// </summary>
    /// <param name="videoFilepath"></param>
    /// <returns></returns>
    public async Task GetTranscriptFromVideo(string videoFilepath, Video videoDb)
    {
        // Convert video file to MP3
        string baseDirectory = Path.Combine(
            _hostEnvironment.WebRootPath,
            "uploads",
            "transcript_tmp");
        Directory.CreateDirectory(baseDirectory);

        // append extension
        string mp3Path = Path.Combine(
            baseDirectory,
            Path.GetFileNameWithoutExtension(videoFilepath) + ".mp3");
        _logger.LogInformation(
            "Converting video to mp3: {Mp3Path}",
            mp3Path);
        FfmpegHelper.GetMp3(videoFilepath, mp3Path);

        // Upload MP3 to AWS Bucket
        // Object name is just the filename
        string objectName = Path.GetFileName(mp3Path);
        string? uploadResult = await UploadFileAsync(objectName, mp3Path);

        if (uploadResult is null)
        {
            _logger.LogError(
                "Error transcribing file {Mp3} to object name {ObjectName}",
                mp3Path, objectName);
            return;
        }

        // Transcribe the mp3 file
        string jobName = Guid.NewGuid().ToString();
        string mediaFileUri = $"s3://{_bucketName}/{objectName}";
        TranscriptionJob transcriptionJob = await StartTranscriptionJob(
            jobName, mediaFileUri, MediaFormat.Mp3, LanguageCode.EnGB, null);

        // Wait for the transcription job to finish
        GetTranscriptionJobRequest jobRequest = new GetTranscriptionJobRequest
        {
            TranscriptionJobName = jobName
        };

        bool done = false;
        while (!done)
        {
            _logger.LogInformation("Waiting for job response...");
            GetTranscriptionJobResponse jobResponse
                = await _transcribeClient.GetTranscriptionJobAsync(jobRequest);

            transcriptionJob = jobResponse.TranscriptionJob;
            if (transcriptionJob.TranscriptionJobStatus
                != TranscriptionJobStatus.IN_PROGRESS)
            {
                done = true;
                _logger.LogInformation("Job is completed");
            }
            else
            {
                await Task.Delay(1000);
            }
        }

        // Read the transcription into a string
        if (transcriptionJob.TranscriptionJobStatus
            == TranscriptionJobStatus.FAILED)
        {
            _logger.LogError("Failed to transcribe: {Error}",
                             transcriptionJob.FailureReason);
            return;
        }

        string outputUri = transcriptionJob.Transcript.TranscriptFileUri;
        string outputFilename
            = Path.Combine(baseDirectory,
                           Path.GetFileNameWithoutExtension(objectName)
                           + ".json");
        
        using (var client = new HttpClient())
        {
            using var stream = client.GetStreamAsync(outputUri);
            await using var fs = new FileStream(outputFilename, FileMode.Create);

            await stream.Result.CopyToAsync(fs);
        }
        
        // Delete the mp3 file after it is done (to save space)
        File.Delete(mp3Path);

        // debug: check your json!;
        string jsonStr = await File.ReadAllTextAsync(outputFilename);
        dynamic result = JsonConvert.DeserializeObject(jsonStr)!;

        string transcript = result.results.transcripts[0].transcript.ToString();

        // var connectionstring = "ConnectionStrings:GrowGreenDB";
        //
        // var optionsBuilder = new DbContextOptionsBuilder<GrowGreenContext>();
        // optionsBuilder.UseSqlServer(connectionstring);

        // using var db = new GrowGreenContext();
        _logger.LogInformation("Saving to db...");
        videoDb.GeneratedTranscript = transcript;

        using var context = _contextFactory.CreateDbContext();
        context.Update(videoDb);
        try
        {
            context.SaveChanges();

        }
        catch (Exception ex)
        {
            _logger.LogError("{Error} {Message}",ex, ex.Message);
        }
        
        _logger.LogInformation("Saved to DB!");
    }

    private async Task SaveToDb(string transcript, int videoId)
    {
        
    }

    /// <summary>
    /// Start a transcription job for a media file. This method returns
    /// as soon as the job is started.
    /// </summary>
    /// <param name="jobName">A unique name for the transcription job.</param>
    /// <param name="mediaFileUri">The URI of the media file, typically an Amazon S3 location.</param>
    /// <param name="mediaFormat">The format of the media file.</param>
    /// <param name="languageCode">The language code of the media file, such as en-US.</param>
    /// <param name="vocabularyName">Optional name of a custom vocabulary.</param>
    /// <returns>A TranscriptionJob instance with information on the new job.</returns>
    private async Task<TranscriptionJob> StartTranscriptionJob(
        string jobName, string mediaFileUri,
        MediaFormat mediaFormat, LanguageCode languageCode,
        string? vocabularyName)
    {
        var response = await _transcribeClient.StartTranscriptionJobAsync(
            new StartTranscriptionJobRequest()
            {
                TranscriptionJobName = jobName,
                Media = new Media()
                {
                    MediaFileUri = mediaFileUri
                },
                MediaFormat = mediaFormat,
                LanguageCode = languageCode,
                Settings = vocabularyName != null
                    ? new Settings()
                    {
                        VocabularyName = vocabularyName
                    }
                    : null
            });
        return response.TranscriptionJob;
    }

    /// <summary>
    /// Shows how to upload a file from the local computer to an Amazon S3
    /// bucket.
    /// </summary>
    /// <param name="client">An initialized Amazon S3 client object.</param>
    /// <param name="bucketName">The Amazon S3 bucket to which the object
    /// will be uploaded.</param>
    /// <param name="objectName">The object to upload.</param>
    /// <param name="filePath">The path, including file name, of the object
    /// on the local computer to upload.</param>
    /// <returns>The URI of the file. If this is null, this
    /// indicates that the upload has failed.</returns>
    private async Task<string?> UploadFileAsync(
        string objectName,
        string filePath)
    {
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = objectName,
            FilePath = filePath,
        };

        var response = await _s3Client.PutObjectAsync(request);
        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        {
            _logger.LogInformation(
                "Successfully uploaded {ObjectName} to {BucketName}",
                objectName, _bucketName);

            return $"s3://{_bucketName}/{objectName}";
        }
        else
        {
            _logger.LogError("Could not upload {ObjectName} to {BucketName}",
                             objectName, _bucketName);
            return null;
        }
    }
}