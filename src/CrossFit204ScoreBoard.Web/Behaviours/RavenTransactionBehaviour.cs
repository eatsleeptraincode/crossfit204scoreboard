using FubuMVC.Core.Behaviors;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Behaviours
{
    public class RavenTransactionBehaviour : BasicBehavior
    {
        readonly IDocumentSession session;

        public RavenTransactionBehaviour(IDocumentSession session)
            : base(PartialBehavior.Ignored)
        {
            this.session = session;
        }

        protected override void afterInsideBehavior()
        {
            session.SaveChanges();
        }
    }
}