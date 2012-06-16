using FubuMVC.Core;
using FubuMVC.Core.Security;

namespace CrossFit204ScoreBoard.Web.Actions.Accounts
{
    public class LoginStatusAction
    {
        private readonly ISecurityContext secContext;

        public LoginStatusAction(ISecurityContext secContext)
        {
            this.secContext = secContext;
        }

        [FubuPartial]
        public LoginStatusViewModel Get(LoginStatusRequest request)
        {
            var user = secContext.CurrentUser;
            return new LoginStatusViewModel();
        }
    }

    public class LoginStatusRequest{}

    public class LoginStatusViewModel{}
}