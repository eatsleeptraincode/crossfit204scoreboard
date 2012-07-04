using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Validation.Rules;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Validation.Rules
{
    [TestFixture]
    public class PasswordsShouldMatchTests : ValidationRuleContext<PasswordsShouldMatch,RegisterViewModel>
    {
        [Test]
        public void WhenPasswordsDontMatchShouldBeInvalid()
        {
            Validate(new RegisterViewModel{Athlete = new Athlete{Password = "PW1"}, ConfirmPassword = "PW2" });
            Notification.IsValid().ShouldBeFalse();
            Notification.FirstErrorKey().ShouldEqual("PasswordsDontMatch");
        }

        [Test]
        public void WhenPasswordsMatchShouldBeValid()
        {
            Validate(new RegisterViewModel{Athlete = new Athlete{Password = "PW1"}, ConfirmPassword = "PW1" });
            Notification.IsValid().ShouldBeTrue();
        }
    }
}