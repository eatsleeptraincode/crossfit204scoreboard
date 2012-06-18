using CrossFit204ScoreBoard.Web.Behaviours;
using FubuMVC.Core.Behaviors;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Behaviours
{
    [TestFixture]
    public class RavenTransactionBehaviourTest : InteractionContext<RavenTransactionBehaviour>
    {
        protected override void beforeEach()
        {
            ClassUnderTest.InsideBehavior = MockFor<IActionBehavior>();
        }

        [Test]
        public void SaveChangesWhenInvoked()
        {
            ClassUnderTest.Invoke();
            Services.Get<IActionBehavior>().AssertWasCalled(b => b.Invoke());
            Services.Get<IDocumentSession>().AssertWasCalled(s => s.SaveChanges());
        }

        [Test]
        public void DontSaveChangesForPartial()
        {
            ClassUnderTest.InvokePartial();
            Services.Get<IActionBehavior>().AssertWasCalled(b => b.InvokePartial());
            Services.Get<IDocumentSession>().AssertWasNotCalled(s => s.SaveChanges());
        }
    }
}