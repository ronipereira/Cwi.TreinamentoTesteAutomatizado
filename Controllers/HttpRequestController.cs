using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
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

        public void AddHeader(string name, string value)
        {
            GetHttpRequestMessage().Headers.Add(name, value);
        }

        public void AddJsonBody(object body)
        {
            GetHttpRequestMessage().Content = PrepareJsonBody(body);
        }

        private HttpContent PrepareJsonBody(object body)
        {
            if (body.GetType().IsPrimitive || body is string)
            {
                return new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            }
            else
            {
                return new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            }
        }

        public async Task SendAsync(string endpoint, string httpMethodName)
        {
            var request = GetHttpRequestMessage();

            request.RequestUri = new Uri(BaseUrl, endpoint);

            request.Method = GetHttpMethodFromName(httpMethodName);

            HttpResponseMessage = await HttpClientFactory.CreateClient().SendAsync(request);

            HttpRequestMessage.Dispose();
            HttpRequestMessage = null;
        }

        public HttpStatusCode GetResponseHttpStatusCode()
        {
            return HttpResponseMessage.StatusCode;
        }

        public async Task<T> GetTypedResponseBody<T>()
        {
            var responseContent = await GetResponseBodyContent();

            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public async Task<string> GetResponseBodyContent()
        {
            using var httpContent = HttpResponseMessage.Content;
            return await httpContent.ReadAsStringAsync();
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
