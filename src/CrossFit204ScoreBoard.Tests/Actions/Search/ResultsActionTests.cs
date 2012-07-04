using CrossFit204ScoreBoard.Web.Actions.Search;
using CrossFit204ScoreBoard.Web.Models;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Actions.Search
{
    [TestFixture]
    public class ResultsActionTests : RavenContextWithIndex<ResultsAction>
    {
        const string SearchTerm = "SearchTerm";
        readonly Athlete athlete1 = new Athlete { FirstName = SearchTerm };
        readonly Athlete athlete2 = new Athlete { LastName = SearchTerm };
        readonly Athlete athlete3 = new Athlete();
        readonly Workout workout1 = new Workout { Name = SearchTerm };
        readonly Workout workout2 = new Workout();

        protected override void BeforeIndexing()
        {
            Session.Store(athlete1);
            Session.Store(athlete2);
            Session.Store(athlete3);
            Session.Store(workout1);
            Session.Store(workout2);
        }

        [Test]
        public void ShouldReturnWorkoutsAndAthletesThatMatch()
        {
            var result = ClassUnderTest.Get(new SearchResultsRequest {Term = SearchTerm});
            result.Athletes.ShouldContain(athlete1);
            result.Athletes.ShouldContain(athlete2);
            result.Athletes.ShouldNotContain(athlete3);
            result.Workouts.ShouldContain(workout1);
            result.Workouts.ShouldNotContain(workout2);
        }
    }
}