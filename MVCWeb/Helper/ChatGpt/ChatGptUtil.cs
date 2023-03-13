using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVCWeb.Helper.ChatGpt
{
    public class ChatGptUtil
    {
        string apiKey = "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";
        public async Task<string> ChatWithGpt(string question)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", apiKey);
            string endpoint = "https://api.openai.com/v1/engines/davinci-codex/completions";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            var requestData = new
            {
                prompt = "Hello, ChatGPT!",
                max_tokens = 5
            };
            string json = JsonConvert.SerializeObject(requestData);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            ChatGtpObject responseObject = JsonConvert.DeserializeObject<ChatGtpObject>(responseContent);

            string ret = responseContent.FirstOrDefault().ToString();
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
