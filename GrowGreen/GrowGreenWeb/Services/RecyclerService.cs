using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.ML;
using static Microsoft.ML.DataOperationsCatalog;
using Microsoft.ML.Vision;

namespace GrowGreenWeb.Services;

public class RecyclerService
{
    static string _assetsPath = Path.Combine(Environment.CurrentDirectory, "assets");
    static string _imagesFolder = Path.Combine(_assetsPath, "images");
    static string _trainTagsTsv = Path.Combine(_imagesFolder, "tags.tsv");
    static string _testTagsTsv = Path.Combine(_imagesFolder, "test-tags.tsv");
    static string _predictSingleImage = Path.Combine(_imagesFolder, "toaster3.jpg");
    static string _inceptionTensorFlowModel = Path.Combine(_assetsPath, "inception", "tensorflow_inception_graph.pb");

    public static void Execute()
    {
        MLContext mlContext = new MLContext();

        
    }
    
    void ClassifySingleImage(MLContext mlContext, ITransformer model)
    {

    }
    
    void DisplayResults(IEnumerable<ImagePrediction> imagePredictionData)
    {
        foreach (ImagePrediction prediction in imagePredictionData)
        {
            Console.WriteLine($"Image: {Path.GetFileName(prediction.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()} ");
        }
    }
}

struct InceptionSettings
{
    public const int ImageHeight = 224;
    public const int ImageWidth = 224;
    public const float Mean = 117;
    public const float Scale = 1;
    public const bool ChannelsLast = true;
}

public class ImageData
{
    public string ImagePath { get; set; }

    public string Label { get; set; }
}

public class ImagePrediction : ImageData
{
    public float[] Score;

    public string PredictedLabelValue;
}