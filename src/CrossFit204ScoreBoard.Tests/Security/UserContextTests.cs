using System.Security.Principal;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Security;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Security
{
    [TestFixture]
    public class UserContextTests : InteractionContext<UserContext>
    {
         [Test]
           public void NoUserReturnNull()
         {
             ClassUnderTest.User.ShouldBeNull();
         }

         [Test]
         public void NotFubuPrincipalReturnNull()
         {
             Services.Get<ISecurityContext>().Stub(c => c.CurrentUser).Return(MockFor<IPrincipal>());
             ClassUnderTest.User.ShouldBeNull();
         }

         [Test]
         public void NotFubuPrincipalReturnAthlete()
         {
             var athlete = new Athlete();
             Services
                 .Get<ISecurityContext>()
                 .Stub(c => c.CurrentUser)
                 .Return(new FubuPrincipal(MockFor<IIdentity>(), athlete));
             ClassUnderTest.User.ShouldEqual(athlete);
         }
    }
}