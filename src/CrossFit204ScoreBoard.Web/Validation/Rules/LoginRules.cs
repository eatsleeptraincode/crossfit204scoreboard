using System.Linq;
using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Validation.Rules
{
    public class LoginRules : ModelRule<LoginViewModel>
    {
        private readonly IEncryptor encryptor;
        private readonly IDocumentSession session;

        public LoginRules(IEncryptor encryptor, IDocumentSession session)
        {
            this.encryptor = encryptor;
            this.session = session;
        }

        protected override void Validate()
        {
            var userName = GetValue(m => m.Athlete.UserName);
            var athlete = session.Query<Athlete>().SingleOrDefault(a => a.UserName == userName);
            if (athlete == null)
                RegisterError("UserDoesntExist", "The UserName you entered doesn't exist", Accessor(m => m.Athlete.Password));
            else
                ValidateEncryptedPasswordsMatch(athlete);
        }

        private void ValidateEncryptedPasswordsMatch(Athlete athlete)
        {
            var newPassword = GetValue(m => m.Athlete.Password);
            var encrypted = encryptor.Encrypt(newPassword);
            var existing = athlete.Password;
            if (encrypted != existing)
                RegisterError("PasswordIncorrect", "The password you enterred is incorrect", Accessor(m => m.Athlete.Password));
        }
    }
}