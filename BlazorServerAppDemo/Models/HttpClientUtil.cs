using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BlazorServerAppDemo.Models
{
    public class HttpClientUtil

    {
        public HttpClientUtil(string _baseUrl)
        {
            baseUrl = _baseUrl;
        }
        private string baseUrl;
        public async Task<M> ConnectHttpClient<R, M>(string apiUrl, R reqModel)
        {
            M model = default(M);

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsJsonAsync(apiUrl, reqModel);
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<M>(res);
            }
            return model;
        }

        //public async Task<M> ConnectRestClient<R, M>(string apiUrl, R reqModel)
        //{
        //    M model = default(M);

        //    RestClient restClient = new RestClient(baseUrl);
        //    RestRequest restRequest = new RestRequest(apiUrl, Method.POST, DataFormat.Json);
        //    restRequest.AddJsonBody(reqModel);
        //    IRestResponse restResponse = await restClient.ExecuteAsync(restRequest);
        //    if (restResponse.IsSuccessful)
        //    {
        //        string response = restResponse.Content;
        //        model = JsonConvert.DeserializeObject<M>(response);
        //    }
        //    return model;
        //}
    }
}
