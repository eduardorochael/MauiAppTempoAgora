using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string Cidade)
        {
            Tempo? t = null;

            string chave = "657f9afae19c5d9dbc9e6d2bdefae687";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                $"q={Cidade}&units=metric&appid={chave}";

            using (HttpClient client = new HttpClient()) 
            {
                HttpResponseMessage resp = await client.GetAsync(url);
                if (resp.IsSuccessStatusCode) 
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunste = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();



                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"]["main"],
                        temp_min = (double)rascunho["main"]["tem_min"],
                        temp_max = (double)rascunho["main"]["tem_max"],
                        speed = (double)rascunho["main"]["speed"],
                        visibility = (int)rascunho["main"]["visibility"],
                        sunrise = sunrise.ToString(),
                        sunset = sunrise.ToString(),
                        

                    };//fecha objeto do tempo 
                }//fecha if se o estatus do servidor 
            }//fecha laço
                     

            return t;
        }
    }
}
