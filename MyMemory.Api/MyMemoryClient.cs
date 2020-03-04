using System;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
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
            var response = await client.GetAsync(
                new Uri(
                    this.baseUri,
                    $"keygen?user={HttpUtility.UrlEncode(username)}&pass={HttpUtility.UrlEncode(password)}"));
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

        public async Task<TranslationResponse> Translate(TranslationRequest request)
        {
            string q = HttpUtility.UrlEncode(request.QueriedText);
            string langPair = HttpUtility.UrlEncode($"{request.SourceLanguage}|{request.TargetLanguage}");
            int mt = request.MachineTranslated ? 1 : 0;
            string key = request.ApiKey.IsAnonymous ? "" : $"&key={HttpUtility.UrlEncode(request.ApiKey.ToString())}";
            int onlyprivate = request.PrivateMatchesOnly ? 1 : 0;
            string ip = request.SourceIp == null ? "" : $"&ip={HttpUtility.UrlEncode(request.SourceIp.ToString())}";
            string de = request.MailAddress == null ? "" : $"&de={HttpUtility.UrlEncode(request.MailAddress.ToString())}";
            var response = await client.GetAsync(
                new Uri(
                    this.baseUri,
                    $"get?q={q}&langpair={langPair}&mt={mt}{key}&onlyprivate={onlyprivate}{ip}{de}"));
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new MyMemoryApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }

        public async Task Set(TranslationSetRequest request)
        {
            string seg = HttpUtility.UrlEncode(request.SourceText);
            string tra = HttpUtility.UrlEncode(request.TranslatedText);
            string langPair = HttpUtility.UrlEncode($"{request.SourceLanguage}|{request.TargetLanguage}");
            string key = request.ApiKey.IsAnonymous ? "" : $"&key={HttpUtility.UrlEncode(request.ApiKey.ToString())}";
            string de = request.MailAddress == null ? "" : $"&de={HttpUtility.UrlEncode(request.MailAddress.ToString())}";
            var response = await client.GetAsync(
                new Uri(
                    this.baseUri,
                    $"set?seg={seg}&tra={tra}&langpair={langPair}{key}{de}"));
            if (response.IsSuccessStatusCode)
            {
                
            }
            else
            {
                throw new MyMemoryApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
            }
        }
    }

    public struct ApiKey : IEquatable<ApiKey>
    {
        public static readonly ApiKey AnonymousAccess = default;

        public bool IsAnonymous => key == null;

        public bool Equals(ApiKey other)
        {
            return String.Equals(key, other.key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ApiKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (key != null ? key.GetHashCode() : 0);
        }

        public static bool operator ==(ApiKey left, ApiKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ApiKey left, ApiKey right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return key;
        }

        private readonly string key;

        public ApiKey(string key)
        {
            this.key = key;
        }
    }
}
