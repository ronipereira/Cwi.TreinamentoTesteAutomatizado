using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cwi.TreinamentoTesteAutomatizado.Controllers
{
    public class HttpRequestController
    {
        private readonly IHttpClientFactory HttpClientFactory;
        private readonly Uri BaseUrl;
        private HttpRequestMessage HttpRequestMessage;
        private HttpResponseMessage HttpResponseMessage;

        public HttpRequestController(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            HttpClientFactory = httpClientFactory;
            BaseUrl = new Uri(baseUrl);
        }

        private HttpRequestMessage GetHttpRequestMessage()
        {
            if (HttpRequestMessage == null)
                HttpRequestMessage = new HttpRequestMessage();
            
            return HttpRequestMessage;
        }

        public void RemoveHeader(string name)
        {
            GetHttpRequestMessage().Headers.Remove(name);
        }

        public async Task SendAsync(string endpoint, string httpMethodName)
        {
            var request = GetHttpRequestMessage();

            request.RequestUri = new Uri(BaseUrl, endpoint);

            request.Method = GetHttpMethodFromName(httpMethodName);

            HttpResponseMessage = await HttpClientFactory.CreateClient().SendAsync(request);
        }

        public HttpStatusCode GetResponseHttpStatusCode()
        {
            return HttpResponseMessage.StatusCode;
        }

        private HttpMethod GetHttpMethodFromName(string httpMethodName)
        {
            switch (httpMethodName.ToLower())
            {
                case "get":
                    return HttpMethod.Get;
                case "post":
                    return HttpMethod.Post;
                case "patch":
                    return HttpMethod.Patch;
                case "put": 
                    return HttpMethod.Put;
                case "delete":
                    return HttpMethod.Delete;
                case "options":
                    return HttpMethod.Options;
                case "head":
                    return HttpMethod.Head;
                default:
                    return HttpMethod.Get;
            }
        }
    }
}
