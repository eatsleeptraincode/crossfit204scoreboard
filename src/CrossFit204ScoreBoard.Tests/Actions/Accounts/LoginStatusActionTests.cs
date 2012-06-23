using System.Security.Principal;
using CrossFit204ScoreBoard.Web.Actions.Accounts;
using FubuMVC.Core.Security;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Actions.Accounts
{
    [TestFixture]
    public class LoginStatusActionTests : InteractionContext<LoginStatusAction>
    {
        private const string UserName = "UserName";

        [Test]
        public void ReturnUserNameFromContext()
        {
            var identity = MockFor<IIdentity>();
            identity.Stub(i => i.Name).Return(UserName);
            Services.Get<ISecurityContext>().Stub(c => c.CurrentIdentity).Return(identity);
            var result = ClassUnderTest.Get(new LoginStatusRequest());
            result.UserName.ShouldEqual(UserName);
        }
    }
}