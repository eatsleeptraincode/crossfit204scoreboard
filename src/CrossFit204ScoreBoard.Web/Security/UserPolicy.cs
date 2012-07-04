using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Security
{
    public class UserPolicy : IAuthorizationPolicy
    {
        private readonly IDocumentSession session;
        private readonly ISecurityContext secContext;

        public UserPolicy(IDocumentSession session, ISecurityContext secContext)
        {
            this.session = session;
            this.secContext = secContext;
        }

        public AuthorizationRight RightsFor(IFubuRequest request)
        {
            var athleteId = request.Get<AthleteSpecific>().AthleteId;
            var athlete = session.Load<Athlete>(athleteId);
        
            if (athlete.UserName == secContext.CurrentIdentity.Name)
                return AuthorizationRight.Allow;
            return AuthorizationRight.Deny;
        }
    }
}