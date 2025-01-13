using final.Models;
using Newtonsoft.Json;

namespace final.Services
{
    public class Api_Service
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private readonly string ApiKey = "82c0383fbd2473ed9c4581f99d90543a";

        public async Task<Root?> GetWeather(double latitude, double longitude)
        {
            string url = string.Format(
                "https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,wind_speed_10m&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m",
                latitude, longitude, ApiKey);

            try
            {
                var response = await HttpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<Root>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<CityRoot?> GetWeatherByCity(string city)
        {
            string url = string.Format(
                "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}",
                city, ApiKey);

            try
            {
                var response = await HttpClient.GetStringAsync(url);
                return JsonConvert.DeserializeObject<CityRoot>(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }



    }
}



