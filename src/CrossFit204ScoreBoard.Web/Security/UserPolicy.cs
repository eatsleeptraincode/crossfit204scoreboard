using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Actions.Scores;
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

            var athleteId = GetAthleteId(request);
            var athlete = session.Load<Athlete>(athleteId);
        
            if (athlete.UserName == secContext.CurrentIdentity.Name)
                return AuthorizationRight.Allow;
            return AuthorizationRight.Deny;
        }

        private string GetAthleteId(IFubuRequest request)
        {
            var input1 = request.Get<DeleteScoreRequest>();
            if (input1 != null)
                return input1.AthleteId;
            return request.Get<EditAthleteRequest>().AthleteId;
        }
    }
}