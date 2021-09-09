﻿using Cwi.TreinamentoTesteAutomatizado.Controllers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.Employee
{
    [Binding]
    public class CreateEmployeeSteps
    {
        private readonly HttpRequestController HttpRequestController;

        public CreateEmployeeSteps(HttpRequestController httpRequestController)
        {
            HttpRequestController = httpRequestController;
        }

        [Given(@"que o usuário não esteja autenticado")]
        public void DadoQueOUsuarioNaoEstejaAutenticado()
        {
            //ScenarioContext.Current.Pending();
        }

        [Given(@"que seja solicitado a criação de um novo funcionário")]
        public async Task DadoQueSejaSolicitadoACriacaoDeUmNovoFuncionario()
        {
            await HttpRequestController.SendAsync("v1/employees", "POST");
        }

        [Then(@"o funcionário não será cadastrado")]
        public void EntaoOFuncionarioNaoSeraCadastrado()
        {
            //ScenarioContext.Current.Pending();
        }

        [Then(@"será retornado uma mensagem de falha de autenticação")]
        public void EntaoSeraRetornadoUmaMensagemDeFalhaDeAutenticacao()
        {
            //ScenarioContext.Current.Pending();
        }

    }
}