using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BlazorServerAppDemo.Data
{
    public class ChatGptService
    {
       
        string baseAddress = "";
        HttpClient _httpClient;
        public ChatGptService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress =new Uri( baseAddress);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
        }
        string apiKey = "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";
        /// <summary>
        /// Hello, ChatGPT!
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public async Task<string> ChatWithGpt(string question)
        {


            string endpoint = "https://api.openai.com/v1/engines/davinci-codex/completions";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            var requestData = new
            {
                prompt = question,
                max_tokens = 15
            };
            string json = JsonConvert.SerializeObject(requestData);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            string ret = "no answer";
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                ChatGtpObject responseObject = JsonConvert.DeserializeObject<ChatGtpObject>(responseContent);
                ret = responseObject.Choices.FirstOrDefault().Text;
            }
            return ret;
        }
    }
    public class Choice
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("logprobs")]
        public object Logprobs { get; set; }

        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
    }

    public class ChatGtpObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; }

        [JsonProperty("usage")]
        public Usage Usage { get; set; }
    }

    public class Usage
    {
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonProperty("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
