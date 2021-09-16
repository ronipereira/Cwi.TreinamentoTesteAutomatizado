using Cwi.TreinamentoTesteAutomatizado.Controllers;
using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.Common
{
    [Binding]
    public class AuthenticationSteps
    {
        private readonly HttpRequestController HttpRequestController;

        public AuthenticationSteps(HttpRequestController httpRequestController, IConfiguration configuration)
        {
            HttpRequestController = httpRequestController;
        }

        [Given(@"que o usuário esteja autenticado")]
        public async Task DadoQueOUsuarioEstejaAutenticado()
        {
            HttpRequestController.AddJsonBody(new { Username = "", Password = "" });

            await HttpRequestController.SendAsync("v1/public/auth", "POST");
        }

        [Given(@"que o usuário não esteja autenticado")]
        public void DadoQueOUsuarioNaoEstejaAutenticado()
        {
            HttpRequestController.RemoveHeader("Authorization");
        }
    }
}
