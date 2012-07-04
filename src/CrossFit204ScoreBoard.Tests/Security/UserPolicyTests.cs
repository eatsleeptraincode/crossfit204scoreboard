using System.Security.Principal;
using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Security
{
    [TestFixture]
    public class UserPolicyTests : RavenContext<UserPolicy>
    {
        private const string AthleteId = "AthleteId";
        private const string UserName = "Name1";
        private const string UserName2 = "Name2";
        private IFubuRequest request;
        private IIdentity identity;

        protected override void OneTimeSetup()
        {
            Session.Store(new Athlete { Id = AthleteId, UserName = UserName });
            Session.SaveChanges();
        }

        protected override void SetupForEach()
        {
            request = MockFor<IFubuRequest>();
            request
                .Stub(r => r.Get<AthleteSpecific>())
                .Return(new AthleteSpecific { AthleteId = AthleteId });

            identity = MockFor<IIdentity>();
            Services.Get<ISecurityContext>().Stub(c => c.CurrentIdentity).Return(identity);
        }

        [Test]
        public void UserMatchesAllowed()
        {
            identity.Stub(i => i.Name).Return(UserName);
            var right = ClassUnderTest.RightsFor(request);
            right.ShouldEqual(AuthorizationRight.Allow);
        }

        [Test]
        public void UserDoesntMatchDenied()
        {
            identity.Stub(i => i.Name).Return(UserName2);
            var right = ClassUnderTest.RightsFor(request);
            right.ShouldEqual(AuthorizationRight.Deny);
        }
    }
}