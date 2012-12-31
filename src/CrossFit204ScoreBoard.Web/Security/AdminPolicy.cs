using System.Linq;
using CrossFit204ScoreBoard.Web.Indexes;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Security
{
    public class AdminPolicy : IAuthorizationPolicy
    {
        private readonly IDocumentSession session;
        private readonly ISecurityContext secContext;

        public AdminPolicy(IDocumentSession session, ISecurityContext secContext)
        {
            this.session = session;
            this.secContext = secContext;
        }

        public AuthorizationRight RightsFor(IFubuRequest request)
        {
            var athlete = session
                .Query<Athlete,AthletesByUserName>()
                .SingleOrDefault(a => a.UserName == secContext.CurrentIdentity.Name);
            if (athlete == null || !athlete.IsAdmin)
                return AuthorizationRight.Deny;
            return AuthorizationRight.Allow;
        }
    }
}