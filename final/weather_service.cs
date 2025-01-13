using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class WeatherService
{
    private const string ApiKey = "4ab4a9534efe4afec120d36c2eefda67"; // API anahtarınızı buraya ekleyin
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

    public async Task<WeatherInfo> GetWeatherAsync(string city)
    {
        using var client = new HttpClient();
        var url = $"{BaseUrl}?q={city}&appid={ApiKey}&units=metric";
        var response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherInfo>(json);
        }
        return null;
    }
}

public class WeatherInfo
{
    public Main Main { get; set; }
    public string Name { get; set; }
}

public class Main
{
    public double Temp { get; set; }
    public int Humidity { get; set; }
}
