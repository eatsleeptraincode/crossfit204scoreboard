using System.Linq;
using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using CrossFit204ScoreBoard.Web.Validation.Rules;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Validation.Rules
{
    [TestFixture]
    public class UserDoesntExist : ValidationRuleContext<LoginRules,LoginViewModel>
    {
        [Test]
        public void ShouldRegisterAnError()
        {
            Validate(new LoginViewModel {Athlete = new Athlete {UserName = "rweppler"}});
            Notification.IsValid().ShouldBeFalse();
            Notification.FirstErrorKey().ShouldEqual("UserDoesntExist");
        }
    }

    [TestFixture]
    public class PasswordIncorrect : ValidationRuleContext<LoginRules, LoginViewModel>
    {
        private const string Newpassword = "NewPassword";
        private const string UserName = "rweppler";

        [Test]
        public void ShouldRegisterAnError()
        {
            Session.Store(new Athlete{UserName = UserName, Password = "EncryptedPassword"});
            Session.SaveChanges();
            Services.Get<IEncryptor>().Stub(e => e.Encrypt(Newpassword)).Return("WrongPassword");
            Validate(new LoginViewModel {Athlete = new Athlete {UserName = UserName, Password = Newpassword}});
            Notification.IsValid().ShouldBeFalse();
            Notification.FirstErrorKey().ShouldEqual("PasswordIncorrect");
        }
    }

    [TestFixture]
    public class PasswordCorrect : ValidationRuleContext<LoginRules, LoginViewModel>
    {
        private const string Newpassword = "NewPassword";
        private const string UserName = "rweppler";
        private const string Encryptedpassword = "EncryptedPassword";

        [Test]
        public void ShouldBeValid()
        {
            Session.Store(new Athlete { UserName = UserName, Password = Encryptedpassword });
            Session.SaveChanges();
            Services.Get<IEncryptor>().Stub(e => e.Encrypt(Newpassword)).Return(Encryptedpassword);
            Validate(new LoginViewModel { Athlete = new Athlete { UserName = UserName, Password = Newpassword } });
            Notification.IsValid().ShouldBeTrue();
        }
    }
}