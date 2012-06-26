using System.Linq;
using System.Security.Principal;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Security;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Security
{
    public class FubuPrincipalFactory : IPrincipalFactory
    {
        readonly IDocumentSession session;

        public FubuPrincipalFactory(IDocumentSession session)
        {
            this.session = session;
        }

        public IPrincipal CreatePrincipal(IIdentity identity)
        {
            var athlete = session
                .Query<Athlete>()
                .SingleOrDefault(a => a.UserName == identity.Name);
            return FubuPrincipal.Current
                ?? new FubuPrincipal(identity, athlete);
        }
    }
}