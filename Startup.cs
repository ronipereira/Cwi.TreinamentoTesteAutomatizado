using BoDi;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado
{
    [Binding]
    public class Startup
    {
        private readonly IObjectContainer ObjectContainer;
        public Startup(IObjectContainer objectContainer)
        {
            ObjectContainer = objectContainer;
        }

        [BeforeScenario]
        public void DependecyInjection()
        {

        }
    }
}
