using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dynamic;
using Newtonsoft.Json;
using Systems.DialogueSystem.WebClient.Bodies;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Systems.DialogueSystem.WebClient
{
    public class DialogueWebClient
    {
        private static readonly HttpClient Client = new();

        private static string AuthorizationKey =
            "OTRjOTJhZTAtNWMwMS00MGUzLWJjZjAtNjQ2MzMxNzkwMjg1OjM3OTVhZmFjLTZmYWMtNDMyZC1iM2RkLWFjMjIxNDMyNTkwZg==";

        private static string AccessToken;
        private static Dictionary<string, List<Message>> _messages;

        static DialogueWebClient()
        {
            _messages = new Dictionary<string, List<Message>>();
        }

        public void AddStartNPCMessage(string message, string npcName)
        {
            if (!_messages.ContainsKey(npcName))
            {
                _messages.Add(npcName, new List<Message>());
            }
            
            _messages[npcName].Add(new Message("assistant", message));
        }
        
        public async Task GetAccessToken()
        {
            string url = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";

            var payload = new Dictionary<string, string>
            {
                { "scope", "GIGACHAT_API_PERS" }
            };

            var content = new FormUrlEncodedContent(payload);

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };

                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("RqUID", "16e817bc-3848-4796-97a8-491bb6b16b57");
                request.Headers.Add("Authorization", $"Basic {AuthorizationKey}");

                using HttpResponseMessage response = await Client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = JsonUtility.FromJson<AccessTokenResponseBody>(responseBody);
                AccessToken = responseJson.access_token;
                DynamicDebug.Debug(nameof(DialogueWebClient), nameof(GetAccessToken), "Access token was received");
                DynamicDebug.Debug(nameof(DialogueWebClient), nameof(GetAccessToken), $"Access token: {AccessToken}");
            }
            catch (HttpRequestException e)
            {
                DynamicDebug.DebugError(nameof(DialogueWebClient), nameof(GetAccessToken), "Exception Caught!");
                DynamicDebug.DebugError(nameof(DialogueWebClient), nameof(GetAccessToken), $"Message :{e.Message}");
            }
        }

        public async Task<string> GetText(string input, string promt, string npcName)
        {
            string url = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions";

            if (!_messages.ContainsKey(npcName))
            {
                _messages.Add(npcName, new List<Message>());
            }

            _messages[npcName].Add(new Message("user", $"{promt}\n{input}"));

            for (int i = 0; i < _messages[npcName].Count; i++)
            {
                Debug.Log(_messages[npcName][i].role + " " + _messages[npcName][i].content);
            }

            var requestBody =
                new RequestBody("GigaChat", _messages[npcName], 1, false, 512, 1, 0, Random.Range(0.15f, 1f));
            var json = JsonConvert.SerializeObject(requestBody);
            Debug.Log(json);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = content
                };

                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", $"Bearer {AccessToken}");

                using HttpResponseMessage response = await Client.SendAsync(request);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = JsonConvert.DeserializeObject<ResponseBody>(responseBody);
                var answer = responseJson.choices[^1].message;
                _messages[npcName].Add(answer);

                foreach (var message in _messages[npcName])
                    Debug.Log(message.role + " " + message.content);
                
                Debug.Log(responseBody);

                return answer.content;
            }
            catch (HttpRequestException e)
            {
                Debug.LogError($"Exception Caught: {e.Message}");
                return null;
            }
        }
    }
}