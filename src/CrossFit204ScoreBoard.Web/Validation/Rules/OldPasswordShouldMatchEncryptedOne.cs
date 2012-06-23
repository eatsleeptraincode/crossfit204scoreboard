using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Security;

namespace CrossFit204ScoreBoard.Web.Validation.Rules
{
    public class OldPasswordShouldMatchEncryptedOne : ModelRule<ChangePasswordViewModel>
    {
        private readonly IEncryptor encryptor;
        private readonly IUserContext context;

        public OldPasswordShouldMatchEncryptedOne(IEncryptor encryptor, IUserContext context)
        {
            this.encryptor = encryptor;
            this.context = context;
        }

        protected override void Validate()
        {
            var oldPassword = GetValue(m => m.OldPassword);
            var oldPasswordEncrypted = encryptor.Encrypt(oldPassword);
            if (oldPasswordEncrypted != context.User.Password)
                RegisterError("Old password doesn't match existing",
                              "The old password you entered doesn't match the existing one",
                              Accessor(m => m.OldPassword));
        }
    }
}