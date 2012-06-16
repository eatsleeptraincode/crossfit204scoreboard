using CrossFit204ScoreBoard.Web.Behaviours;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Behaviours
{
    [TestFixture]
    public class RavenTransactionBehaviourTest : InteractionContext<RavenTransactionBehaviour>
    {
        [Test]
        public void SaveChangesWhenInvoked()
        {
            ClassUnderTest.Invoke();
            Services.Get<IDocumentSession>().AssertWasCalled(s => s.SaveChanges());
        }

        [Test]
        public void DontSaveChangesForPartial()
        {
            ClassUnderTest.InvokePartial();
            Services.Get<IDocumentSession>().AssertWasNotCalled(s => s.SaveChanges());
        }
    }
}