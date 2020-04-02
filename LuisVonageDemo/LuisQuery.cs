using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using System;
using System.Threading.Tasks;


namespace LuisVonageDemo
{
    public class LuisQuery
    {

        // Use Language Understanding (LUIS) prediction endpoint key
        // to create authentication credentials
        private static string _predictionKey = Environment.GetEnvironmentVariable("LUIS_PREDICTION_KEY");

        // Endpoint URL example value = "https://YOUR-RESOURCE-NAME.api.cognitive.microsoft.com"
        private static string _predictionEndpoint = Environment.GetEnvironmentVariable("LUIS_ENDPOINT_NAME");

        // App Id example value e.g. "df67dcdb-c37d-46af-88e1-8b97951ca1c2"
        private static string _appId = Environment.GetEnvironmentVariable("LUIS_APP_ID");



        private static LUISRuntimeClient CreateClient()
        {
            var credentials = new ApiKeyServiceClientCredentials(_predictionKey);
            return new LUISRuntimeClient(credentials, new System.Net.Http.DelegatingHandler[] { })
            {
                Endpoint = _predictionEndpoint
            };
        }

        public static async Task<PredictionResponse> GetPredictionAsync(string query)
        {
            using (var luisClient = CreateClient())
            {
                var requestOptions = new PredictionRequestOptions
                {
                    PreferExternalEntities = true
                };

                var predictionRequest = new PredictionRequest
                {
                    Query = query,
                    Options = requestOptions
                };                

                return await luisClient.Prediction.GetSlotPredictionAsync(
                    Guid.Parse(_appId),
                    slotName: "production",
                    predictionRequest,
                    verbose: true,
                    showAllIntents: true,
                    log: true);
            }
        }
    }
}
