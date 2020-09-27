using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace MyMemory.Api
{
    public class MyMemoryClient
    {
        private readonly HttpClient client;

        private readonly Uri baseUri;

        public MyMemoryClient(Uri baseUri = null, HttpClient client = null)
        {
            this.client = client ?? new HttpClient();
            this.baseUri = baseUri ?? new Uri("https://api.mymemory.translated.net");
        }

        public async Task<ApiKey> GenerateKey(string username, string password)
        {
            using (var response = await client.GetAsync(new Uri(this.baseUri, $"keygen?user={HttpUtility.UrlEncode(username)}&pass={HttpUtility.UrlEncode(password)}")))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<KeyGenResponse>(await response.Content.ReadAsStringAsync());
                    return new ApiKey(result.Key);
                }
                else
                {
                    throw new MyMemoryApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<TranslationResponse> Translate(TranslationRequest request)
        {
            string q = HttpUtility.UrlEncode(request.QueriedText);
            string langPair = HttpUtility.UrlEncode($"{request.SourceLanguage}|{request.TargetLanguage}");
            int mt = request.MachineTranslated ? 1 : 0;
            string key = request.ApiKey.IsAnonymous ? "" : $"&key={HttpUtility.UrlEncode(request.ApiKey.ToString())}";
            int onlyprivate = request.PrivateMatchesOnly ? 1 : 0;
            string ip = request.SourceIp == null ? "" : $"&ip={HttpUtility.UrlEncode(request.SourceIp.ToString())}";
            string de = request.MailAddress == null ? "" : $"&de={HttpUtility.UrlEncode(request.MailAddress.ToString())}";
            using (var response = await client.GetAsync(new Uri(this.baseUri, $"get?q={q}&langpair={langPair}&mt={mt}{key}&onlyprivate={onlyprivate}{ip}{de}")))
            {
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new MyMemoryApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task Set(TranslationSetRequest request)
        {
            string seg = HttpUtility.UrlEncode(request.SourceText);
            string tra = HttpUtility.UrlEncode(request.TranslatedText);
            string langPair = HttpUtility.UrlEncode($"{request.SourceLanguage}|{request.TargetLanguage}");
            string key = request.ApiKey.IsAnonymous ? "" : $"&key={HttpUtility.UrlEncode(request.ApiKey.ToString())}";
            string de = request.MailAddress == null ? "" : $"&de={HttpUtility.UrlEncode(request.MailAddress.ToString())}";
            using (var response = await client.GetAsync(new Uri(this.baseUri, $"set?seg={seg}&tra={tra}&langpair={langPair}{key}{de}")))
            {
                if (response.IsSuccessStatusCode)
                {
                
                }
                else
                {
                    throw new MyMemoryApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task Import(ImportRequest request)
        {
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(request.Input), "tmx", request.FileName);

                if (request.Description != null)
                {
                    content.Add(new StringContent(request.Description, Encoding.UTF8), "name");
                }

                if (request.Subject != null)
                {
                    content.Add(new StringContent(request.Subject, Encoding.UTF8), "subj");
                }

                if (request.IsPrivate != null)
                {
                    content.Add(new StringContent(request.IsPrivate.Value ? "1" : "0", Encoding.UTF8), "private");
                }

                if (!request.ApiKey.IsAnonymous)
                {
                    content.Add(new StringContent(request.ApiKey.ToString(), Encoding.UTF8), "key");
                }

                if (request.SourceUrl != null)
                {
                    content.Add(new StringContent(request.SourceUrl.ToString(), Encoding.UTF8), "surl");
                }

                if (request.TargetUrl != null)
                {
                    content.Add(new StringContent(request.TargetUrl.ToString(), Encoding.UTF8), "turl");
                }

                using (var response =
                    await client.PostAsync(
                        new Uri(
                            this.baseUri,
                            $"tmx/import"),
                        content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        
                    }
                    else
                    {
                        throw new MyMemoryApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
                    }
                }
            }
        }

        public async Task<StatusResponse> Status(StatusRequest request)
        {
            var key = HttpUtility.UrlEncode(request.ApiKey.ToString());
            var name = request.Description == null ? "" : $"&name={HttpUtility.UrlEncode(request.Description)}";
            var status = request.Status == null ? "" : $"&status={HttpUtility.UrlEncode(((int)request.Status.Value).ToString())}";
            using (var response = await client.GetAsync(new Uri(this.baseUri, $"tmx/status?key={key}{name}{status}")))
            {
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<StatusResponse>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    throw new MyMemoryApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
                }
            }
        }

        public async Task<IEnumerable<string>> ListSubjects()
        {
            var response = await client.GetAsync(
                new Uri(
                    this.baseUri,
                    $"subjects"));
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<string>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new MyMemoryApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }
    }
}
