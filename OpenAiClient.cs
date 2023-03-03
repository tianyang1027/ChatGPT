using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatGPT
{
    public class OpenAiClient
    {
        private readonly HttpClient _httpClient;

        public OpenAiClient(string apiKey)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> SendRequest(string messages, string model)
        {
            var requestParams = new
            {
                messages = new List<object>() { new { role = "user", content = messages } },
                model = model,
            };

            var requestJson = JsonSerializer.Serialize(requestParams);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

            // api documents : https://platform.openai.com/docs/api-reference/chat/create

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);

            return response.Content.ReadAsStringAsync().Result;
        }
    }

}
