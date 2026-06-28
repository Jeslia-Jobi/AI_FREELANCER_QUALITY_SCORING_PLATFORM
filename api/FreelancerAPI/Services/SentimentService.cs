using Microsoft.ML;
using Microsoft.ML.Data;
using FreelancerAPI.DTOs;

namespace FreelancerAPI.Services
{
    public class SentimentService
    {
        private readonly PredictionEngine<
            SentimentInputWithLabel,
            SentimentPrediction
        > _predictionEngine;

        public SentimentService()
        {
            var mlContext = new MLContext();

            DataViewSchema schema;

            var model = mlContext.Model.Load(
                "AIModels/sentiment-model.zip",
                out schema
            );

            // Create a schema definition with all required columns including the dummy Label
            var inputSchema = SchemaDefinition.Create(typeof(SentimentInputWithLabel));
            var outputSchema = SchemaDefinition.Create(typeof(SentimentPrediction));
            
            _predictionEngine =
                mlContext.Model.CreatePredictionEngine
                <
                    SentimentInputWithLabel,
                    SentimentPrediction
                >(model, ignoreMissingColumns: true, inputSchemaDefinition: inputSchema, outputSchemaDefinition: outputSchema);
        }

        public SentimentResultDto Analyze(
            string feedback
        )
        {
            var prediction =
                _predictionEngine.Predict(
                    new SentimentInputWithLabel
                    {
                        Feedback = feedback,
                        Label = ""  // Dummy value
                    }
                );

            int score =
                prediction.PredictedLabel switch
                {
                    "VeryPositive" => 10,
                    "Positive" => 7,
                    "Mixed" => 3,
                    "Neutral" => 0,
                    "Negative" => -5,
                    "VeryNegative" => -10,
                    _ => 0
                };

            return new SentimentResultDto
            {
                SentimentCategory =
                    prediction.PredictedLabel,

                SentimentScore =
                    score
            };
        }
    }

    public class SentimentInput
    {
        public string Feedback { get; set; } = "";
    }

    public class SentimentInputWithLabel
    {
        public string Feedback { get; set; } = "";
        
        [ColumnName("Label")]
        public string Label { get; set; } = "";
    }

    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public string PredictedLabel { get; set; } = "";

        public float[] Score { get; set; } = [];
    }
}