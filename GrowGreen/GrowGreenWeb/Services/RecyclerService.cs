using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace GrowGreenWeb.Services;

public class RecyclerService
{
    private readonly AmazonRekognitionClient _client;

    public RecyclerService()
    {
        string accessKey =
            Environment.GetEnvironmentVariable("AWS_REKOGNITION_ACCESS_KEY")!;
        string secretKey =
            Environment.GetEnvironmentVariable("AWS_REKOGNITION_SECRET_KEY")!;
        string region = Environment.GetEnvironmentVariable("AWS_REGION")!;

        var credentials =
            new Amazon.Runtime.BasicAWSCredentials(accessKey, secretKey);
        var regionEndpoint = Amazon.RegionEndpoint.GetBySystemName(region);

        _client = new AmazonRekognitionClient(credentials, regionEndpoint);
    }

    public async Task<DetectLabelsResponse> GetLabels(string imagePath)
    {
        Image image = new Image
        {
            Bytes = new MemoryStream(await LoadFile(imagePath))
        };

        DetectLabelsRequest request = new DetectLabelsRequest
        {
            Image = image,
            MaxLabels = 10,
            MinConfidence = 80F
        };

        return await _client.DetectLabelsAsync(request);
    }

    private async Task<byte[]> LoadFile(string path)
    {
        await using var fs =
            new FileStream(path, FileMode.Open, FileAccess.Read);
        byte[] data = new byte[fs.Length];
        _ = await fs.ReadAsync(data, 0, (int)fs.Length);
        return data;
    }
}