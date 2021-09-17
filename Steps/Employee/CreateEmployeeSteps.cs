using Cwi.TreinamentoTesteAutomatizado.Controllers;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.Employee
{
    [Binding]
    [Scope(Tag = "CreateEmployee")]
    public class CreateEmployeeSteps
    {
        private readonly HttpRequestController HttpRequestController;

        public CreateEmployeeSteps(HttpRequestController httpRequestController)
        {
            HttpRequestController = httpRequestController;
        }

        [Given(@"que seja solicitado a criação de um novo funcionário")]
        public async Task DadoQueSejaSolicitadoACriacaoDeUmNovoFuncionario()
        {
            HttpRequestController.AddJsonBody(new { Name = "Funcionario 1", Email = "funcionario1@empresa.com.br" });
            await HttpRequestController.SendAsync("v1/employees", "POST");
        }

        [Given(@"que seja solicitado a criação de um novo funcionário sem o preenchimento dos campos obrigatórios")]
        public async Task DadoQueSejaSolicitadoACriacaoDeUmNovoFuncionarioSemOPreenchimentoDosCamposObrigatorios()
        {
            HttpRequestController.AddJsonBody(new { });
            await HttpRequestController.SendAsync("v1/employees", "POST");
        }

        [Then(@"o funcionário não será cadastrado")]
        public void EntaoOFuncionarioNaoSeraCadastrado()
        {
            Assert.That(HttpRequestController.GetResponseHttpStatusCode(), Is.Not.EqualTo(HttpStatusCode.Created));
        }

        [Then(@"será retornado uma mensagem de falha de autenticação")]
        public void EntaoSeraRetornadoUmaMensagemDeFalhaDeAutenticacao()
        {
            Assert.That(HttpRequestController.GetResponseHttpStatusCode(), Is.EqualTo(HttpStatusCode.Unauthorized));
        }

        [Then(@"será retornado uma mensagem de falha de preenchimentos de campos obrigatórios")]
        public void EntaoSeraRetornadoUmaMensagemDeFalhaDePreenchimentosDeCamposObrigatorios()
        {
            Assert.That(HttpRequestController.GetResponseHttpStatusCode(), Is.EqualTo(HttpStatusCode.BadRequest));
        }


        [Then(@"o funcionário será cadastrado")]
        public void EntaoOFuncionarioSeraCadastrado()
        {
            Assert.That(HttpRequestController.GetResponseHttpStatusCode(), Is.EqualTo(HttpStatusCode.Created));
        }
    }
}
