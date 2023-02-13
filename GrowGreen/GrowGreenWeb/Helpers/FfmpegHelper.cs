using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GrowGreenWeb.Helpers;

public static class FfmpegHelper
{
    private const string FFMPEG_PATH_MAC = "../ExternalLibraries/ffmpeg/ffmpeg-mac-amd64";
    private const string FFMPEG_PATH_LINUX = "../ExternalLibraries/ffmpeg/ffmpeg-linux-amd64";
    private const string FFMPEG_PATH_WIN = "../ExternalLibraries/ffmpeg/ffmpeg-win.exe";

    private static string FFMPEG_PATH
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return FFMPEG_PATH_MAC;
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return FFMPEG_PATH_LINUX;
            else
                return FFMPEG_PATH_WIN;
        }
    }
    // get a thumbnail from a video file
    public static void GetThumbnail(string video, string outputJpgPath, string? velicina)
    {
        // velicina means the size of the thumbnail
        if (velicina == null)
            velicina = "640x360";

        Exec(video, outputJpgPath, "-s " + velicina);
    }

    /// <summary>
    /// converts a video to mp3
    /// </summary>
    /// <param name="videoPath"></param>
    /// <param name="outputMp3Path">Must contain .mp3 extension</param>
    public static void GetMp3(string videoPath, string outputMp3Path)
    {
        Exec(videoPath, outputMp3Path, null);
    }

    private static void Exec(string input, string output, string? parametri)
    {
        Process ffmpeg = new Process();

        Console.WriteLine("ffmpeg: current directory is " + Directory.GetCurrentDirectory());
        Console.WriteLine("ffmpeg: output image file is " + output);
        ffmpeg.StartInfo.Arguments = $" -i \"{input}\" {(parametri != null ? " " + parametri : "")} \"{output}\"";
        ffmpeg.StartInfo.FileName = FFMPEG_PATH;
        ffmpeg.StartInfo.UseShellExecute = false;
        ffmpeg.StartInfo.RedirectStandardOutput = true;
        ffmpeg.StartInfo.RedirectStandardError = true;
        ffmpeg.StartInfo.CreateNoWindow = true;

        Console.WriteLine(ffmpeg.StartInfo.FileName + " " + ffmpeg.StartInfo.Arguments);
        ffmpeg.Start();
        while (!ffmpeg.StandardOutput.EndOfStream)
            Console.WriteLine("ffmpeg: " + ffmpeg.StandardOutput.ReadLine());
        
        ffmpeg.WaitForExit();
        ffmpeg.Close();
    }
}