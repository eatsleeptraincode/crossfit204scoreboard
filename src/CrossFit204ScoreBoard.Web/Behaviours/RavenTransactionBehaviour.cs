using FubuMVC.Core.Behaviors;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Behaviours
{
    public class RavenTransactionBehaviour : IActionBehavior
    {
        readonly IDocumentSession session;
        public IActionBehavior InsideBehavior { get; set; }

        public RavenTransactionBehaviour(IDocumentSession session)
        {
            this.session = session;
        }

        public void Invoke()
        {
            using (session)
            {
                InsideBehavior.Invoke();
                session.SaveChanges();
            }
        }

        public void InvokePartial()
        {
            InsideBehavior.InvokePartial();
        }
    }
}