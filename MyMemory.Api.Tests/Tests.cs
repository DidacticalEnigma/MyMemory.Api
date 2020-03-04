using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MyMemory.Api;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        private MyMemoryClient api;

        [SetUp]
        public void Setup()
        {
            api = new MyMemoryClient(null, new HttpClient(new MyMemoryStubHttpHandler()));
        }

        [Test]
        public async Task Test()
        {
            var result = await api.Translate(new TranslationRequest("Hello World!", "en", "it"));
            Assert.AreEqual("Ciao Mondo!", result.Matches.Single().Translation);
        }

        [Explicit]
        [Test]
        public async Task RecordRequest()
        {
            api = new MyMemoryClient();

            var result = await api.Translate(new TranslationRequest("Hello World!", "en", "it"));
            Assert.AreEqual("Ciao Mondo!", result.Matches.Single().Translation);
        }
    }
}