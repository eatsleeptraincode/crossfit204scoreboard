using CrossFit204ScoreBoard.Web.Actions;
using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Actions.Accounts
{
    [TestFixture]
    public class LoginActionTests : InteractionContext<LoginAction>
    {
        private Athlete athlete = new Athlete {UserName = "UserName"};
        private FubuContinuation result;

        protected override void beforeEach()
        {
            result = ClassUnderTest.Post(new LoginViewModel {Athlete = athlete});
        }

        [Test]
        public void MarkUserAsAuthenticated()
        {
            Services
                .Get<IAuthenticationContext>()
                .AssertWasCalled(c => c.ThisUserHasBeenAuthenticated(athlete.UserName, true));
        }

        [Test]
        public void RedirectToHomePage()
        {
            result.AssertWasRedirectedTo<ScoreBoardRequest>(r => true);
        }
    }
}