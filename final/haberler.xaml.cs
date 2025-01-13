using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.Maui.Controls;
using Newtonsoft.Json.Linq;

namespace final
{
    public partial class haberler : ContentPage
    {


        public ObservableCollection<Haber> HaberListesi { get; set; } = new ObservableCollection<Haber>();
        public ObservableCollection<Kategori> Kategoriler { get; set; } = new ObservableCollection<Kategori>(Kategori.liste);
        public  haberler()
        {
            InitializeComponent();
            NewsList.ItemsSource = HaberListesi; 
            CategoryCollectionView.ItemsSource = Kategoriler;
            
        }

        
        private async void OnCategorySelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Kategori selectedCategory)
            {
                await DisplayAlert("Kategori Seçildi", $"Seçilen Kategori: {selectedCategory.Baslik}", "Tamam");

                
                string jsonData = await HaberleriGetir(selectedCategory);
                if (!string.IsNullOrEmpty(jsonData))
                {
                    ParseHaberler(jsonData);
                }
            }
        }

  
        public static async Task<string> HaberleriGetir(Kategori ctg)
        {
            try
            {
                HttpClient client = new HttpClient();
              
                string url = $"https://api.rss2json.com/v1/api.json?rss_url={ctg.Link}";
                using HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsondata = await response.Content.ReadAsStringAsync();
                return jsondata;
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Hata", "Haberleri yüklerken bir hata oluþtu.", "Tamam");
                return null;
            }
        }

     
        private void ParseHaberler(string jsonData)
        {
            JObject json = JObject.Parse(jsonData);
            var items = json["items"];
            HaberListesi.Clear();

            foreach (var item in items)
            {
                Haber haber = new Haber
                {
                    Title = (string)item["title"],
                    PubDate = (string)item["pubDate"],
                    Description = (string)item["description"]
                };
                HaberListesi.Add(haber);
            }
        }


        private async void OnRefreshClicked(object sender, EventArgs e)
        {
            if (CategoryCollectionView.SelectedItem is Kategori selectedCategory)
            {
                string jsonData = await HaberleriGetir(selectedCategory);
                if (!string.IsNullOrEmpty(jsonData))
                {
                    ParseHaberler(jsonData);
                }
            }
        }
    }


    public class Haber
    {
        public string Title { get; set; }
        public string PubDate { get; set; }
        public string Description { get; set; }
    }


    public class Kategori
    {
        public string Baslik { get; set; }
        public string Link { get; set; }

        public static List<Kategori> liste = new List<Kategori>()
             {
             new Kategori() { Baslik = "Manþet", Link = "https://www.trthaber.com/manset_articles.rss"},
             new Kategori() { Baslik = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss"},
             new Kategori() { Baslik = "Gündem", Link = "https://www.trthaber.com/gundem_articles.rss"},
             new Kategori() { Baslik = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss"},
             new Kategori() { Baslik = "Spor", Link = "https://www.trthaber.com/spor_articles.rss"},
             new Kategori() { Baslik = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss"},
             new Kategori() { Baslik = "Güncel", Link = "https://www.trthaber.com/guncel_articles.rss"},
             new Kategori() { Baslik = "Eðitim", Link = "https://www.trthaber.com/egitim_articles.rss"},
             };

    }
}