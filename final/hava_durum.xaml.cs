using final.Services;
using System;

namespace final;

public partial class hava_durum : ContentPage
{
    private readonly Api_Service _apiService;

    public hava_durum(Api_Service apiService)
    {
        BackgroundColor = SettingsService.GetBackgroundColor();
        InitializeComponent();
        _apiService = apiService;
        
    }

    public hava_durum() : this(new Api_Service())
    {
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        
        double latitude = 41.0082; 
        double longitude = 28.9784; 
        var result = await _apiService.GetWeather(latitude, longitude);

        if (result != null)
        { 

          
            LblCity.Text = result. + " 📍";
            WeatherDesc.Text = result.current.weather[0].description;
            LblMain.Text = result.current.weather[0].main;
            LblTemper.Text = ((int)result.current.temp).ToString() + " °C";
            imageIcon.Source = result.current.weather[0].IconUrl;
            LblWind.Text = result.current.wind_speed.ToString() + " km/h";
            humadty.Text = result.current.humidity.ToString() + " %";
        }
        else
        {
            await DisplayAlert("Error", "Failed to fetch weather data.", "OK");
        }
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        string response = await DisplayPromptAsync("Search Weather", "Enter city name:", "Search", "Cancel");

        if (!string.IsNullOrEmpty(response))
        {
            var result2 = await _apiService.GetWeatherByCity(response);
            Console.WriteLine(result2);
            if (result2 != null)
            {
                LblCity.Text = result2.name + " 📍";
                WeatherDesc.Text = result2.weather[0].description;
                LblMain.Text = result2.weather[0].main;
                LblTemper.Text = ((int)result2.main.temp).ToString() + " °C";
                imageIcon.Source = result2.weather[0].IconUrl;
                LblWind.Text = result2.wind.speed.ToString() + " km/h";
                humadty.Text = result2.main.humidity.ToString() + " %";
            }
            else
            {
                await DisplayAlert("Error", "Failed to fetch weather data for the given city.", "OK");
            }
        }
    }
}
