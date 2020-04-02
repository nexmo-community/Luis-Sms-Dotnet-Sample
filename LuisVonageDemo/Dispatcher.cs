using Newtonsoft.Json.Linq;
using Nexmo.Api;
using System;

namespace LuisVonageDemo
{
    public class Dispatcher
    {
        public enum Intent 
        {
            None,
            OrderFood
        }

        public static async void ExecuteQuery(SMS.SMSInbound inbound)
        {
            try
            {
                var query = inbound.text;

                var apiKey = Environment.GetEnvironmentVariable("NEXMO_API_KEY");
                var apiSecret = Environment.GetEnvironmentVariable("NEXMO_API_SECRET");

                var message = string.Empty;
                var pred = await LuisQuery.GetPredictionAsync(query);
                var intent = Enum.Parse(typeof(Intent), pred.Prediction.TopIntent);
                Console.WriteLine($"Top intent was {pred.Prediction.TopIntent}");
                switch (intent)
                {
                    case Intent.None:
                        message = "I didn't quite get that. Can you please specify what you would like to do?";
                        break;
                    case Intent.OrderFood:
                        var food = (pred.Prediction.Entities["Food"] as JArray)?[0];
                        var restaraunt = (pred.Prediction.Entities["RestaurantReservation.PlaceName"] as JArray)?[0];
                        message = $"We'll have that {food} from {restaraunt} send over straight away!";
                        break;
                }

                Console.WriteLine($"Message: {message}");
                var client = new Client(new Nexmo.Api.Request.Credentials { ApiKey = apiKey, ApiSecret = apiSecret });
                client.SMS.Send(new SMS.SMSRequest { to = inbound.msisdn, from = inbound.to, text = message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
