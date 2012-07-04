using CrossFit204ScoreBoard.Web.Actions.Workouts;
using CrossFit204ScoreBoard.Web.Models;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Actions.Workouts
{
    [TestFixture]
    public class DetailActionTests : RavenContextWithIndex<DetailsAction>
    {
        readonly Workout workout = new Workout();

        protected override void BeforeIndexing()
        {
            Session.Store(workout);

            var maleAthlete = new Athlete { Gender = Gender.Male };
            var femaleAthlete = new Athlete { Gender = Gender.Female };
            Session.Store(maleAthlete);
            Session.Store(femaleAthlete);

            Session.Store(new Score { WorkoutId = workout.Id, AthleteId = maleAthlete.Id });
            Session.Store(new Score { WorkoutId = workout.Id, AthleteId = femaleAthlete.Id });
            Session.Store(new Score());
        }

        [Test]
        public void ShouldReturnWorkoutAndAllRelatedScores()
        {
            var result = ClassUnderTest.Get(new WorkoutDetailsRequest{WorkoutId = workout.Id});
            result.Item.Workout.ShouldEqual(workout);
            result.Item.MenScores.ShouldHaveCount(1);
            result.Item.WomenScores.ShouldHaveCount(1);
        }
    }
}