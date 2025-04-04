using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;
using System.Net;

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
                try //usar o try 
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
                            description = (string)rascunho["weather"][0]["description"],//descrição 
                            main = (string)rascunho["weather"][0]["main"],
                            temp_min = (double)rascunho["main"]["temp_min"],
                            temp_max = (double)rascunho["main"]["temp_max"],
                            speed = (double)rascunho["wind"]["speed"],//velocidade vento 
                            visibility = (int)rascunho["visibility"],//vizibilidade 
                            sunrise = sunrise.ToString(),
                            sunset = sunset.ToString(),
                        }; // Fecha obj do Tempo.
                           // Fecha if se o status do servidor foi de sucesso

                    }
                    else if (resp.StatusCode == HttpStatusCode.NotFound) // usar elif para os StatusCode
                    {
                        throw new Exception("Cidade não encontrada. Verifique o nome e tente novamente."); //Pop up cidade não encontrada 
                    }
                    else
                    {
                        throw new Exception($"Erro ao buscar previsão: {resp.ReasonPhrase}"); //mensagem de erro
                    }
                }
                catch (HttpRequestException) //Utilizando o catch para enviar mensagem de não conexão 
                {
                    throw new Exception("Falha na conexão com a internet. Verifique sua conexão e tente novamente.");
                }

            }   // fecha laço using  


            return t;
        }
    }
}
