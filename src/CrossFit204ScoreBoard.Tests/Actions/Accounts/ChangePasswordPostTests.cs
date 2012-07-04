using CrossFit204ScoreBoard.Web.Actions;
using CrossFit204ScoreBoard.Web.Actions.Accounts;
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
    public class ChangePasswordGetTests : InteractionContext<ChangePasswordAction>
    {
        [Test]
        public void ShouldReturnDataForView()
        {
            var result = ClassUnderTest.Get(new ChangePasswordRequest());
            result.ShouldBeOfType<ChangePasswordViewModel>();
        }
    }

    [TestFixture]
    public class ChangePasswordPostTests : InteractionContext<ChangePasswordAction>
    {
        private readonly Athlete athlete = new Athlete();
        private FubuContinuation result;
        private const string NewPassword = "New Password";
        private const string EncryptedPassword = "New Password";

        protected override void beforeEach()
        {
            Services.Get<IUserContext>().Stub(c => c.User).Return(athlete);
            Services.Get<IEncryptor>().Stub(e => e.Encrypt(NewPassword)).Return(EncryptedPassword);
            result = ClassUnderTest.Post(new ChangePasswordViewModel {NewPassword = NewPassword});
        }

        [Test]
        public void GetUserFromContext()
        {
            Services.Get<IUserContext>().AssertWasCalled(c => c.User);
        }

        [Test]
        public void EncryptPassword()
        {
            Services.Get<IEncryptor>().AssertWasCalled(e => e.Encrypt(NewPassword));
            athlete.Password.ShouldEqual(EncryptedPassword);
        }

        [Test]
        public void SaveUser()
        {
            Services.Get<IDocumentSession>().AssertWasCalled(s => s.Store(athlete));
        }

        [Test]
        public void RedirectToHomePage()
        {
            result.AssertWasRedirectedTo<ScoreBoardRequest>(r => true);
        }
    }
}