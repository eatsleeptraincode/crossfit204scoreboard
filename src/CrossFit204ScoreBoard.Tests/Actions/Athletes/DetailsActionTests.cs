using System.Linq;
using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client.Embedded;

namespace CrossFit204ScoreBoard.Tests.Actions.Athletes
{
    [TestFixture]
    public class DetailsActionTests
    {
        [Test]
        public void ReturnAllAthletesScores()
        {
            using (var store = new EmbeddableDocumentStore { RunInMemory = true })
            {
                store.Configuration.RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true;
                store.Initialize();
                using (var session = store.OpenSession())
                {
                    session.Store(new Athlete());
                    session.Store(new Athlete());
                    session.SaveChanges();
                    var action = new ListAction(session);
                    var result = action.Get(new AthleteListRequest());
                    result.Athletes.Count().ShouldEqual(2);
                }
            }
        }
         
    }
}