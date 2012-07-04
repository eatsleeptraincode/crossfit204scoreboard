using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Validation.Rules;
using NUnit.Framework;
using FubuTestingSupport;

namespace CrossFit204ScoreBoard.Tests.Validation.Rules
{
    [TestFixture]
    public class UserNameIsUniqueTests : ValidationRuleContext<UserNameIsUnique, RegisterViewModel>
    {
         [Test]
        public void DuplicateUserNameIsInvalid()
         {
             Session.Store(new Athlete{UserName = "duplicate"});
             Session.SaveChanges();
             Validate(new RegisterViewModel{Athlete = new Athlete{UserName = "duplicate"}});
             Notification.IsValid().ShouldBeFalse();
             Notification.FirstErrorKey().ShouldEqual("UserIsNotUnique");
         }

         [Test]
         public void UniqueUserNameIsValid()
         {
             Validate(new RegisterViewModel { Athlete = new Athlete { UserName = "unique" } });
             Notification.IsValid().ShouldBeTrue();
         }
    }
}