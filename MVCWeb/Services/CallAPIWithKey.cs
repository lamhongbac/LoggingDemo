using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCWeb.Services
{
    /// <summary>
    /// https://www.c-sharpcorner.com/article/using-api-key-authentication-to-secure-asp-net-core-web-api/
    /// </summary>
    public class APIServices
    {
        string baseURL = "https://localhost:7008/";
        string url = "weatherforecast";
        //"dGhpc29Db21wYW55OnRoaXNvTWFsbA=="
        string apiKey = "dGhpc29Db21wYW55OnRoaXNvTWFsbA=";
        public async Task<string> GetWeather()
        {
            try
            {
                WeatherForecast rawWeather = new WeatherForecast();
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new System.Uri(baseURL);

                //httpClient.DefaultRequestHeaders.Add("XApiKey", apiKey);
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if (response.StatusCode== System.Net.HttpStatusCode.Unauthorized)
                {
                    return "UnAuthorized";
                }
                string jsonResponse = "Unknown";
                if (response.StatusCode== System.Net.HttpStatusCode.OK)
                {
                    response.EnsureSuccessStatusCode();
                    jsonResponse = await response.Content.ReadAsStringAsync();

                    return jsonResponse;
                }
                

                //string jsonResponse = await response.Content.ReadAsStringAsync();

                //rawWeather = JsonConvert.DeserializeObject<WeatherForecast>(jsonResponse);

                return jsonResponse;
            }catch (Exception ex)
            {
                string err = ex.Message;

                throw;
            }
        }

    }
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
    //public class Rootobject
    //{
    //    public Coord coord { get; set; }
    //    public Weather[] weather { get; set; }
    //    public string _base { get; set; }
    //    public Main main { get; set; }
    //    public int visibility { get; set; }
    //    public Wind wind { get; set; }
    //    public Clouds clouds { get; set; }
    //    public int dt { get; set; }
    //    public Sys sys { get; set; }
    //    public int id { get; set; }
    //    public string name { get; set; }
    //    public int cod { get; set; }
    //}
}
