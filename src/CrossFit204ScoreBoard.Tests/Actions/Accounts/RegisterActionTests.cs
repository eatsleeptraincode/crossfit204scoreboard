using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Continuations;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Actions.Accounts
{
    [TestFixture]
    public class RegisterActionTests : InteractionContext<RegisterAction>
    {
        private Athlete athlete = new Athlete();
        private FubuContinuation result;

        protected override void beforeEach()
        {
            result = ClassUnderTest.Post(new RegisterViewModel { Athlete = athlete });
        }

        [Test]
        public void StoreAthlete()
        {
            Services.Get<IDocumentSession>().AssertWasCalled(s => s.Store(athlete));
        }
        [Test]
        public void RedirectToAthleteList()
        {
            result.AssertWasRedirectedTo<AthleteListRequest>(r => true);
        }
    }
}