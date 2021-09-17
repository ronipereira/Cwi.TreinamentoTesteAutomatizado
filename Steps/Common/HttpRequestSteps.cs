using Cwi.TreinamentoTesteAutomatizado.Controllers;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.Common
{
    [Binding]
    public class HttpRequestSteps
    {
        private readonly HttpRequestController HttpRequestController;

        public HttpRequestSteps(HttpRequestController httpRequestController)
        {
            HttpRequestController = httpRequestController;
        }

        [When(@"realizar uma chama do tipo '(.*)' para o endpoint '(.*)'")]
        public async Task QuandoRealizarUmaChamaDoTipoParaOEndpoint(string httpMethodName, string endpoint)
        {
            await HttpRequestController.SendAsync(endpoint, httpMethodName);
        }


        [When(@"realizar uma chama do tipo '(.*)' para o endpoint '(.*)' com o corpo da requisição")]
        public async Task QuandoRealizarUmaChamaDoTipoParaOEndpointComOCorpoDaRequisicao(string httpMethodName, string endpoint, string body)
        {
            HttpRequestController.AddJsonBody(body);

            await HttpRequestController.SendAsync(endpoint, httpMethodName);
        }

        [Then(@"o código do retorno será '(.*)'")]
        public void EntaoOCodigoDoRetornoSera(int httpStatusCode)
        {
            Assert.That((int)HttpRequestController.GetResponseHttpStatusCode(), Is.EqualTo(httpStatusCode));
        }
    }
}
