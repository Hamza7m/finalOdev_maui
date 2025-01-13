using final.Models;
using final.Services;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;
using System;
using System.Net;
using Newtonsoft.Json.Linq;
namespace final;

public partial class kurlar : ContentPage
{
	public kurlar()
	{
        BackgroundColor = SettingsService.GetBackgroundColor();
		InitializeComponent();
        
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        string url = "https://hasanadiguzel.com.tr/api/kurgetir";
        string jsondata = await Services.KurlarServis.LoadJsonKurData(url);


        var root = JsonSerializer.Deserialize<KurRoot>(jsondata);

        listKurlar.ItemsSource = root.TCMB_AnlikKurBilgileri;
    }
}