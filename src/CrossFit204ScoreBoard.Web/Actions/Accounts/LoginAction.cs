using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;

namespace CrossFit204ScoreBoard.Web.Actions.Accounts
{
    public class LoginAction
    {
        private readonly IAuthenticationContext authContext;

        public LoginAction(IAuthenticationContext authContext)
        {
            this.authContext = authContext;
        }

        public LoginViewModel Get(LoginRequest request)
        {
            return new LoginViewModel();
        }

        public FubuContinuation Post(LoginViewModel request)
        {
            authContext.ThisUserHasBeenAuthenticated(request.Athlete.UserName, true);
            return FubuContinuation.RedirectTo(new ScoreBoardRequest());
        }
    }

    public class LoginRequest
    {
    }

    public class LoginViewModel
    {
        public Athlete Athlete { get; set; }
    }
}