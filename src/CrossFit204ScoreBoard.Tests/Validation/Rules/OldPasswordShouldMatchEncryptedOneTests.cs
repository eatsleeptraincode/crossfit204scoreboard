using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using CrossFit204ScoreBoard.Web.Validation.Rules;
using NUnit.Framework;
using Rhino.Mocks;
using FubuTestingSupport;

namespace CrossFit204ScoreBoard.Tests.Validation.Rules
{   
    [TestFixture]
    public class OldPasswordShouldMatchEncryptedOneTests : ValidationRuleContext<OldPasswordShouldMatchEncryptedOne, ChangePasswordViewModel>
    {
        [Test]
        public void MismatchedPasswordsIsInvalid()
        {
            Services.Get<IEncryptor>().Stub(e => e.Encrypt("OldPassword")).Return("WrongPassword");
            Services.Get<IUserContext>().Stub(c => c.User).Return(new Athlete {Password = "EncryptedPassword"});
            Validate(new ChangePasswordViewModel{OldPassword = "OldPassword"});
            Notification.IsValid().ShouldBeFalse();
            Notification.FirstErrorKey().ShouldEqual("OldPasswordDoesntMatchEncryptedOne");
        }

        [Test]
        public void MatchingPasswordsIsValid()
        {
            Services.Get<IEncryptor>().Stub(e => e.Encrypt("OldPassword")).Return("EncryptedPassword");
            Services.Get<IUserContext>().Stub(c => c.User).Return(new Athlete { Password = "EncryptedPassword" });
            Validate(new ChangePasswordViewModel { OldPassword = "OldPassword" });
            Notification.IsValid().ShouldBeTrue();
        }
    }
}