using Microsoft.ML;
using Microsoft.ML.Data;

namespace AITrainer;

public class SentimentData
{
    [LoadColumn(0)]
    public string Feedback { get; set; } = "";

    [LoadColumn(1)]
    public string Label { get; set; } = "";
}

public class SentimentPrediction
{
    [ColumnName("PredictedLabel")]
    public string Prediction { get; set; } = "";

    public float[] Score { get; set; } = [];
}

internal class Program
{
    static void Main(string[] args)
    {
        var mlContext = new MLContext();

        Console.WriteLine("Loading dataset...");

        IDataView data =
            mlContext.Data.LoadFromTextFile<SentimentData>(
                path: "reviews.csv",
                hasHeader: true,
                separatorChar: ','
            );

        Console.WriteLine("Building pipeline...");

        var pipeline =
            mlContext.Transforms.Text.FeaturizeText(
                outputColumnName: "Features",
                inputColumnName: nameof(SentimentData.Feedback)
            )
            .Append(
                mlContext.Transforms.Conversion.MapValueToKey(
                    outputColumnName: "Label",
                    inputColumnName: nameof(SentimentData.Label)
                )
            )
            .Append(
                mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                    labelColumnName: "Label",
                    featureColumnName: "Features"
                )
            )
            .Append(
                mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel")
            );

        Console.WriteLine("Training model...");

        var model = pipeline.Fit(data);

        mlContext.Model.Save(
            model,
            data.Schema,
            "sentiment-model.zip"
        );

        Console.WriteLine("Model saved successfully!");
    }
}