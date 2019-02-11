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
            if (request.Method == HttpMethod.Get)
            {
                ;
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        "{\"responseData\":{\"translatedText\":\"Ciao Mondo!\",\"match\":1},\"quotaFinished\":false,\"mtLangSupported\":null,\"responseDetails\":\"\",\"responseStatus\":200,\"responderId\":\"235\",\"exception_code\":null,\"matches\":[{\"id\":\"585976534\",\"segment\":\"Hello World!\",\"translation\":\"Ciao Mondo!\",\"quality\":\"74\",\"reference\":null,\"usage-count\":97,\"subject\":\"All\",\"created-by\":\"MateCat\",\"last-updated-by\":\"MateCat\",\"create-date\":\"2019-02-07 07:23:50\",\"last-update-date\":\"2019-02-07 07:23:50\",\"match\":1}]}")
                });
            }
            else
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.MethodNotAllowed));
            }
        }
    }
}