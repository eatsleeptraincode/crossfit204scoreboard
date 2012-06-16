using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;

namespace CrossFit204ScoreBoard.Web.Actions.Accounts
{
    public class LogoutAction
    {
        private readonly IAuthenticationContext authContext;

        public LogoutAction(IAuthenticationContext authContext)
        {
            this.authContext = authContext;
        }

        public FubuContinuation Get(LogoutRequest request)
        {
            authContext.SignOut();
            return FubuContinuation.RedirectTo(new ScoreBoardRequest());
        }
    }

    public class LogoutRequest
    {
    }
}