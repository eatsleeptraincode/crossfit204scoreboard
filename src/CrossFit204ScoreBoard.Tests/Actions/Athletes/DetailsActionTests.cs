using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Actions.Athletes
{
    [TestFixture]
    public class DetailsActionTests : RavenContextWithIndex<DetailsAction>
    {
        readonly Athlete athlete = new Athlete();
        protected override void BeforeIndexing()
        {
            Session.Store(athlete);
            Session.Store(new Score { AthleteId = athlete.Id });
            Session.Store(new Score { AthleteId = athlete.Id });
        }

        [Test]
        public void ReturnAllAthletesScores()
        {
            var result = ClassUnderTest.Get(new AthleteDetailsRequest{AthleteId = athlete.Id});
            result.Athlete.ShouldEqual(athlete);
            result.Scores.ShouldHaveCount(2);
        }
    }
}