using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Validation.Rules;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Validation.Rules
{
    [TestFixture]
    public class NewPasswordShouldMatchTests : ValidationRuleContext<NewPasswordsShouldMatch, ChangePasswordViewModel>
    {
        [Test]
        public void WhenPasswordsDontMatchShouldBeInvalid()
        {
            Validate(new ChangePasswordViewModel {NewPassword = "PW1", ConfirmNewPassword = "PW2"});
            Notification.IsValid().ShouldBeFalse();
            Notification.FirstErrorKey().ShouldEqual("NewPasswordsDontMatch");
        }

        [Test]
        public void WhenPasswordsMatchShouldBeValid()
        {
            Validate(new ChangePasswordViewModel { NewPassword = "PW1", ConfirmNewPassword = "PW1" });
            Notification.IsValid().ShouldBeTrue();
        }
    }
}