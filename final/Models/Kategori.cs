using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Models
{
    public class Kategori
    {
        public string Baslik { get; set; }
        public string Link { get; set; }
        public static List<Kategori> liste = new List<Kategori>()
             {
             new Kategori() { Baslik = "Manşet", Link = "https://www.trthaber.com/manset_articles.rss"},
             new Kategori() { Baslik = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss"},
             new Kategori() { Baslik = "Gündem", Link = "https://www.trthaber.com/gundem_articles.rss"},
             new Kategori() { Baslik = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss"},
             new Kategori() { Baslik = "Spor", Link = "https://www.trthaber.com/spor_articles.rss"},
             new Kategori() { Baslik = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss"},
             new Kategori() { Baslik = "Güncel", Link = "https://www.trthaber.com/guncel_articles.rss"},
             new Kategori() { Baslik = "Eğitim", Link = "https://www.trthaber.com/egitim_articles.rss"},
             };

    }
}
