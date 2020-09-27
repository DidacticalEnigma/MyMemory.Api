using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Tests
{
    public class MyMemoryStubHttpHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // get?q=まいったな&langpair=en|ja
            if (request.Method == HttpMethod.Get)
            {
                return GetRequest(request, cancellationToken);
            }
            else
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }
        }

        private Task<HttpResponseMessage> GetRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri == new Uri("https://api.mymemory.translated.net/get?q=Hello+World!&langpair=en|it&mt=1&onlyprivate=0"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "{\"responseData\":{\"translatedText\":\"Ciao Mondo!\",\"match\":1},\"quotaFinished\":false,\"mtLangSupported\":null,\"responseDetails\":\"\",\"responseStatus\":200,\"responderId\":\"235\",\"exception_code\":null,\"matches\":[{\"id\":\"585976534\",\"segment\":\"Hello World!\",\"translation\":\"Ciao Mondo!\",\"quality\":\"74\",\"reference\":null,\"usage-count\":97,\"subject\":\"All\",\"created-by\":\"MateCat\",\"last-updated-by\":\"MateCat\",\"create-date\":\"2019-02-07 07:23:50\",\"last-update-date\":\"2019-02-07 07:23:50\",\"match\":1}]}")
                });
            }

            if (request.RequestUri == new Uri("https://api.mymemory.translated.net/get?q=Hello+World!&langpair=en|it&mt=1&onlyprivate=1"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "{\"responseData\":{\"translatedText\":\"Ciao mondo!\",\"match\":0.85},\"quotaFinished\":false,\"mtLangSupported\":null,\"responseDetails\":\"\",\"responseStatus\":200,\"responderId\":\"45\",\"exception_code\":null,\"matches\":[{\"id\":0,\"segment\":\"Hello World!\",\"translation\":\"Ciao mondo!\",\"source\":\"English\",\"target\":\"Italian\",\"quality\":70,\"reference\":\"Machine Translation provided by Google, Microsoft, Worldlingo or MyMemory customized engine.\",\"usage-count\":1,\"subject\":false,\"created-by\":\"MT!\",\"last-updated-by\":\"MT!\",\"create-date\":\"2020-03-04 20:04:28\",\"last-update-date\":\"2020-03-04 20:04:28\",\"match\":0.85,\"model\":\"neural\"}]}")
                });
            }

            if (request.RequestUri == new Uri("https://api.mymemory.translated.net/get?q=Hello+World!&langpair=en|it&mt=1&onlyprivate=1&key=asdf"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "{\"responseData\":{\"translatedText\":\"AUTHENTICATION FAILURE. TIP: DO NOT USE 'KEY' AND 'USER' IF YOU DON'T NEED TO ACCESS YOUR PRIVATE MEMORIES. TO CREATE A VALID ACCOUNT, PLEASE DO THE FOLLOWING: 1) REGISTER AS A TRANSLATOR HERE: HTTPS:\\/\\/TRANSLATED.NET\\/TOP\\/ ; 2) GET AN API KEY HERE: HTTPS:\\/\\/MYMEMORY.TRANSLATED.NET\\/DOC\\/KEYGEN.PHP\"},\"quotaFinished\":null,\"mtLangSupported\":null,\"responseDetails\":\"AUTHENTICATION FAILURE. TIP: DO NOT USE 'KEY' AND 'USER' IF YOU DON'T NEED TO ACCESS YOUR PRIVATE MEMORIES. TO CREATE A VALID ACCOUNT, PLEASE DO THE FOLLOWING: 1) REGISTER AS A TRANSLATOR HERE: HTTPS:\\/\\/TRANSLATED.NET\\/TOP\\/ ; 2) GET AN API KEY HERE: HTTPS:\\/\\/MYMEMORY.TRANSLATED.NET\\/DOC\\/KEYGEN.PHP\",\"responseStatus\":\"403\",\"responderId\":null,\"exception_code\":null,\"matches\":\"\"}")
                });
            }

            if (request.RequestUri == new Uri("https://api.mymemory.translated.net/subjects"))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "[\"Accounting\",\"Aerospace\",\"Agriculture_and_Farming\",\"Archeology\",\"architecture\",\"Art\",\"Astronomy\",\"Automotive_Industry\",\"Banking\",\"Chemical\",\"Civil_Engineering\",\"Computer_Science\",\"Credit_Management\",\"Culinary\",\"Finances\",\"Forestry\",\"General\",\"History\",\"Insurance\",\"Legal_and_Notarial\",\"Literary_Translations\",\"Marketing\",\"Matematics_and_Physics\",\"Mechanical\",\"Medical\",\"Music\",\"Nautica\",\"Pharmaceuticals\",\"Religion\",\"Science\",\"Social_Science\",\"Tourism\"]")
                });
            }

            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent("")
            });

            var response403 = @"{
                  ""responseData"": {
                    ""translatedText"": ""'JP' IS AN INVALID TARGET LANGUAGE . EXAMPLE: LANGPAIR=EN|IT USING 2 LETTER ISO OR RFC3066 LIKE ZH-CN. ALMOST ALL LANGUAGES SUPPORTED BUT SOME MAY HAVE NO CONTENT""
                  },
                  ""quotaFinished"": null,
                  ""mtLangSupported"": null,
                  ""responseDetails"": ""'JP' IS AN INVALID TARGET LANGUAGE . EXAMPLE: LANGPAIR=EN|IT USING 2 LETTER ISO OR RFC3066 LIKE ZH-CN. ALMOST ALL LANGUAGES SUPPORTED BUT SOME MAY HAVE NO CONTENT"",
                  ""responseStatus"": ""403"",
                  ""responderId"": null,
                  ""exception_code"": null,
                  ""matches"": """"
                }";
            }

        
    }
}