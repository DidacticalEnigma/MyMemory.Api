using System;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyMemory.Api
{
    public class MyMemoryClient
    {
        private readonly HttpClient client;

        public MyMemoryClient(HttpClient client = null)
        {
            this.client = client ?? new HttpClient();
        }

        public Task<ApiKey> GenerateKey(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<TranslationResponse> Translate(TranslationRequest request)
        {
            var response = await client.GetAsync("https://api.mymemory.translated.net/get?q=Hello%20world&langpair=en|it");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TranslationResponse>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                // TODO
                throw new Exception();
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

        private readonly string key;

        public ApiKey(string key)
        {
            this.key = key;
        }
    }
}
