using CrossFit204ScoreBoard.Web.Actions.Accounts;

namespace CrossFit204ScoreBoard.Web.Validation.Rules
{
    public class NewPasswordsShouldMatch : ModelRule<ChangePasswordViewModel>
    {
        protected override void Validate()
        {
            var newPassword = GetValue(m => m.NewPassword);
            var confirmedPassword = GetValue(m => m.ConfirmNewPassword);
            if (newPassword != confirmedPassword)
                RegisterError("New passwords don't match","The two new passwords you entered don't match", Accessor(m => m.ConfirmNewPassword));
        }
    }
}