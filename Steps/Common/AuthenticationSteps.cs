using Cwi.TreinamentoTesteAutomatizado.Controllers;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.Common
{
    [Binding]
    public class AuthenticationSteps
    {
        private readonly HttpRequestController HttpRequestController;

        public AuthenticationSteps(HttpRequestController httpRequestController)
        {
            HttpRequestController = httpRequestController;
        }

        [Given(@"que o usuário esteja autenticado")]
        public void DadoQueOUsuarioEstejaAutenticado()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"que o usuário não esteja autenticado")]
        public void DadoQueOUsuarioNaoEstejaAutenticado()
        {
            HttpRequestController.RemoveHeader("Authorization");
        }
    }
}
