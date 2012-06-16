using CrossFit204ScoreBoard.Web.Actions;
using CrossFit204ScoreBoard.Web.Actions.Accounts;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Actions.Accounts
{
    [TestFixture]
    public class LogoutActionTests : InteractionContext<LogoutAction>
    {
        private FubuContinuation result;

        protected override void beforeEach()
        {
            result = ClassUnderTest.Get(new LogoutRequest());
        }

        [Test]
        public void SignOut()
        {
            Services.Get<IAuthenticationContext>().AssertWasCalled(c => c.SignOut());
        }

        [Test]
        public void RedirectToHomePage()
        {
            result.AssertWasRedirectedTo<ScoreBoardRequest>(r => true);
        }
    }
}