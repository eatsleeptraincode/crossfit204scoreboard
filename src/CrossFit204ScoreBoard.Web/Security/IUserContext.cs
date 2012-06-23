using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Security;

namespace CrossFit204ScoreBoard.Web.Security
{
    public interface IUserContext
    {
        Athlete User { get; }
    }

    public class UserContext : IUserContext
    {
        private readonly ISecurityContext context;

        public UserContext(ISecurityContext context)
        {
            this.context = context;
        }

        public Athlete User
        {
            get
            {
                var principal = context.CurrentUser as FubuPrincipal;
                return principal != null ? principal.User : null;
            }
        }
    }
}