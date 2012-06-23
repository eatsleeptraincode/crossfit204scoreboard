using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
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
        [Test]
        public void GetAthlete()
        {
            var athlete = new Athlete {Id = "Id"};
            ClassUnderTest.Post(new EditAthleteViewModel { Athlete = athlete });
            Services.Get<IDocumentSession>().AssertWasCalled(s => s.Load<Athlete>(athlete.Id));
        }
    }
}