using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Security;

namespace CrossFit204ScoreBoard.Web.Validation.Rules
{
    public class OldPasswordShouldMatchEncryptedOne : ModelRule<ChangePasswordViewModel>
    {
        private readonly IEncryptor encryptor;

        public OldPasswordShouldMatchEncryptedOne(IEncryptor encryptor)
        {
            this.encryptor = encryptor;
        }

        protected override void Validate()
        {
            var oldPassword = GetValue(m => m.OldPassword);
            var oldPasswordEncrypted = encryptor.Encrypt(oldPassword);
            if (oldPasswordEncrypted != FubuPrincipal.Current.User.Password)
                RegisterError("Old password doesn't match existing",
                              "The old password you entered doesn't match the existing one",
                              Accessor(m => m.OldPassword));
        }
    }
}