using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Services
{
    public static class KurlarServis
    {
        public static async Task<string> LoadJsonKurData(string url)
        {
            string jsonData = "";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
                jsonData = await response.Content.ReadAsStringAsync();

            return jsonData;
        }


        public static async Task<string> loadXmlTCMBKurData(DateTime date)
        {

            //https://www.tcmb.gov.tr/kurlar/202411/01112024.xml
            string url = $"https://www.tcmb.gov.tr/kurlar/{date.ToString("yyyyMM")}/{date.ToString("ddMMyyyy")}.xml";


            string xmlData = "";
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
                xmlData = await response.Content.ReadAsStringAsync();

            return xmlData;

        }
    }
}
