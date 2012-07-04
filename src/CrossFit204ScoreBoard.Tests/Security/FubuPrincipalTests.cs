using System.Security.Principal;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Security
{
    [TestFixture]
    public class FubuPrincipalTests : RavenContext<FubuPrincipalFactory>
    {
        [Test]
        public void BuildPrincipalForUser()
        {
            const string userName = "rweppler";
            var athlete = new Athlete {UserName = userName};
            Session.Store(athlete);
            Session.SaveChanges();

            var identity = MockFor<IIdentity>();
            identity.Stub(i => i.Name).Return(userName);
            
            var principal = ClassUnderTest.CreatePrincipal(identity);
            principal.ShouldBeOfType<FubuPrincipal>()
                .User.ShouldEqual(athlete);

            principal.Identity.ShouldEqual(identity);
            principal.IsInRole("").ShouldBeTrue();
        }
    }
}