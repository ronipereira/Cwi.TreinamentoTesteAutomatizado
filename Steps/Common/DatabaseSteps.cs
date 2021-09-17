using Cwi.TreinamentoTesteAutomatizado.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Cwi.TreinamentoTesteAutomatizado.Steps.Common
{
    [Binding]
    public class DatabaseSteps
    {
        private readonly PostgreDatabaseController PostgreDatabaseController;

        public DatabaseSteps(PostgreDatabaseController postgreDatabaseController)
        {
            PostgreDatabaseController = postgreDatabaseController;
        }

        [Given(@"que a base de dados esteja limpa")]
        public async Task DadoQueABaseDeDadosEstejaLimpa()
        {
            await PostgreDatabaseController.ClearDatabase();
        }

        [Given(@"que os seguintes registros estejam inseridos na tabela '(.*)'")]
        public async Task DadoQueOsSeguintesRegistrosEstejamInseridosNaTabela(string tableName, Table table)
        {
            await PostgreDatabaseController.InsertDatabase(tableName, table);
        }


        [Then(@"o registro estará disponível na tabela '(.*)' da base de dados")]
        public async Task EntaoORegistroEstaraDisponivelNaTabelaDaBaseDeDados(string tableName, Table table)
        {
            var currentItens = await PostgreDatabaseController.SelectFrom(tableName, table);

            Assert.That(currentItens.Count(), Is.Not.Zero, $"Não foram encontrados registros na tabela {tableName}.");

            var actualJsonResponse = JsonConvert.SerializeObject(currentItens);
            var expectedJsonResponse = JsonConvert.SerializeObject(table.CreateDynamicSet()).Replace("'", "");

            Assert.That(JToken.DeepEquals(JToken.Parse(actualJsonResponse), JToken.Parse(expectedJsonResponse)), Is.True, $"Conteúdo atual do retorno \n{actualJsonResponse} diferente do esperado \n{expectedJsonResponse}.");
        }
    }
}
