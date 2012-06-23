using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Continuations;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Accounts
{
    public class ChangePasswordAction
    {
        private readonly IDocumentSession session;
        private readonly IEncryptor encryptor;
        private readonly IUserContext context;

        public ChangePasswordAction(IDocumentSession session, IEncryptor encryptor, IUserContext context)
        {
            this.session = session;
            this.encryptor = encryptor;
            this.context = context;
        }

        public ChangePasswordViewModel Get(ChangePasswordRequest request)
        {
            return new ChangePasswordViewModel();
        }

        public FubuContinuation Post(ChangePasswordViewModel request)
        {
            var athlete = context.User;
            athlete.Password = encryptor.Encrypt(request.NewPassword);
            session.Store(athlete);
            return FubuContinuation.RedirectTo(new ScoreBoardRequest());
        }
    }

    public class ChangePasswordRequest
    {
    }

    public class ChangePasswordViewModel
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}