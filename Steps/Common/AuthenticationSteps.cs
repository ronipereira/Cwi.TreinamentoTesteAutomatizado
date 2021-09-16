using Cwi.TreinamentoTesteAutomatizado.Controllers;
using Cwi.TreinamentoTesteAutomatizado.Models;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.Common
{
    [Binding]
    public class AuthenticationSteps
    {
        private readonly HttpRequestController HttpRequestController;
        private readonly IConfiguration Configuration;

        public AuthenticationSteps(HttpRequestController httpRequestController, IConfiguration configuration)
        {
            HttpRequestController = httpRequestController;
            Configuration = configuration;
        }

        [Given(@"que o usuário esteja autenticado")]
        public async Task DadoQueOUsuarioEstejaAutenticado()
        {
            HttpRequestController.AddJsonBody(new { Username = Configuration["Authentication:Username"], Password = Configuration["Authentication:Password"] });

            await HttpRequestController.SendAsync("v1/public/auth", "POST");

            Assert.That(HttpRequestController.GetResponseHttpStatusCode(), Is.EqualTo(HttpStatusCode.OK));

            var authenticationResponse = await HttpRequestController.GetTypedResponseBody<AuthenticationResponse>();

            HttpRequestController.AddHeader("Authorization", $"Bearer {authenticationResponse.AccessToken}");
        }

        [Given(@"que o usuário não esteja autenticado")]
        public void DadoQueOUsuarioNaoEstejaAutenticado()
        {
            HttpRequestController.RemoveHeader("Authorization");
        }
    }
}
