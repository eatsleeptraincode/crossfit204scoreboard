using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Security;
using NUnit.Framework;
using FubuTestingSupport;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Security
{
    [TestFixture]
    public class AuthenticationPolicyTests : RavenContext<AuthenticationPolicy>
    {
        [Test]
        public void AuthenticatedShouldAllow()
        {
            Services.Get<ISecurityContext>().Stub(c => c.IsAuthenticated()).Return(true);
            var right = ClassUnderTest.RightsFor(null);
            right.ShouldEqual(AuthorizationRight.Allow);
        }

        [Test]
        public void NotAuthenticatedShouldDeny()
        {
            Services.Get<ISecurityContext>().Stub(c => c.IsAuthenticated()).Return(false);
            var right = ClassUnderTest.RightsFor(null);
            right.ShouldEqual(AuthorizationRight.Deny);
        }
    }
}