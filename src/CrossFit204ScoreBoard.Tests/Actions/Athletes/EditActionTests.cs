using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Continuations;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Actions.Athletes
{
    [TestFixture]
    public class EditActionGetTests : InteractionContext<EditAction>
    {
        [Test]
        public void GetAthlete()
        {
            string athleteId = "Id";
            ClassUnderTest.Get(new EditAthleteRequest {AthleteId = athleteId});
            Services.Get<IDocumentSession>().AssertWasCalled(s => s.Load<Athlete>(athleteId));
        }
    }

    [TestFixture]
    public class EditActionPostTests : InteractionContext<EditAction>
    {
        Athlete athlete = new Athlete { Id = "Id" };
        private FubuContinuation result;

        protected override void beforeEach()
        {
            Services.Get<IDocumentSession>().Stub(s => s.Load<Athlete>(athlete.Id)).Return(athlete);
            result = ClassUnderTest.Post(new EditAthleteViewModel { Athlete = athlete });
        }

        [Test]
        public void GetAthlete()
        {
            Services.Get<IDocumentSession>().AssertWasCalled(s => s.Load<Athlete>(athlete.Id));
        }

        [Test]
        public void RedirectToAthleteDetails()
        {
            result.AssertWasRedirectedTo<AthleteDetailsRequest>(r => r.AthleteId == athlete.Id);
        }
    }
}