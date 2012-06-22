using CrossFit204ScoreBoard.Web.Actions.Accounts;

namespace CrossFit204ScoreBoard.Web.Validation.Rules
{
    public class PasswordsShouldMatch : ModelRule<RegisterViewModel>
    {
        protected override void Validate()
        {
            var password = GetValue(m => m.Athlete.Password);
            var confirmPassword = GetValue(m => m.ConfirmPassword);

            if (password != confirmPassword)
                RegisterError("Passwords don't match", "The two passwords you entered don't match", Accessor(m => m.ConfirmPassword));
        }
    }
}