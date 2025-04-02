using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string Cidade)
        {
            Tempo? t = null;

            string chave = "657f9afae19c5d9dbc9e6d2bdefae687"; //declarar chave de api 

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                $"q={Cidade}&units=metric&appid={chave}"; // usar a url do site de api 

            using (HttpClient client = new HttpClient()) // metodo de http 
            {
                HttpResponseMessage resp = await client.GetAsync(url);// utilizando metodo assincruno 

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                        sunrise = sunrise.ToString(),
                        sunset = sunset.ToString(),
                    }; // Fecha obj do Tempo.
                } // Fecha if se o status do servidor foi de sucesso
            } // fecha laço using

            return t;
        }
    }
}
