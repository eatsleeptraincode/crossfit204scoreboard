using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Continuations;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Actions.Accounts
{

    [TestFixture]
    public class RegisterActionGetTests : InteractionContext<RegisterAction>
    {
        [Test]
        public void ShouldReturnDataForView()
        {
            var result = ClassUnderTest.Get(new RegisterRequest());
            result.ShouldBeOfType<RegisterViewModel>();
        }
    }

    [TestFixture]
    public class RegisterActionPostTests : InteractionContext<RegisterAction>
    {
        private const string Password = "Password";
        private const string EncryptedPassword = "EncryptedPassword";
        private readonly Athlete athlete = new Athlete {Password = Password};
        private FubuContinuation result;

        protected override void beforeEach()
        {
            Services.Get<IEncryptor>().Stub(e => e.Encrypt(Password)).Return(EncryptedPassword);
            result = ClassUnderTest.Post(new RegisterViewModel { Athlete = athlete });
        }

        [Test]
        public void EncryptPassword()
        {
            Services.Get<IEncryptor>().AssertWasCalled(e => e.Encrypt(Password));
            athlete.Password.ShouldEqual(EncryptedPassword);
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