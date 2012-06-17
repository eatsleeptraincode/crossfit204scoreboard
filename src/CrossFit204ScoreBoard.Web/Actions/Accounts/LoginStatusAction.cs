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
            var userName = secContext.CurrentIdentity.Name;
            return new LoginStatusViewModel{UserName = userName};
        }
    }

    public class LoginStatusRequest{}

    public class LoginStatusViewModel
    {
        public string UserName { get; set; }
    }
}