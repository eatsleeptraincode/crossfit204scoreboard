using System.Linq;
using CrossFit204ScoreBoard.Web.Actions;
using CrossFit204ScoreBoard.Web.Models;
using NUnit.Framework;
using FubuTestingSupport;

namespace CrossFit204ScoreBoard.Tests.Actions
{
    [TestFixture]
    public class ScoreBoardActionTests : RavenContextWithIndex<ScoreBoardAction>
    {
        protected override void BeforeIndexing()
        {
            var maleAthlete = new Athlete {Gender = Gender.Male};
            Session.Store(maleAthlete);
            var femaleAthlete = new Athlete {Gender = Gender.Female};
            Session.Store(femaleAthlete);
            var workout1 = new Workout();
            Session.Store(workout1);
            var workout2 = new Workout();
            Session.Store(workout2);

            Session.Store(new Score{AthleteId = maleAthlete.Id, WorkoutId = workout1.Id});
            Session.Store(new Score{AthleteId = maleAthlete.Id, WorkoutId = workout2.Id});
            Session.Store(new Score{AthleteId = femaleAthlete.Id, WorkoutId = workout1.Id});
            Session.Store(new Score{AthleteId = femaleAthlete.Id, WorkoutId = workout2.Id});
        }

        [Test]
        public void ShouldReturnAllScoresGroupedByWorkout()
        {
            var result = ClassUnderTest.Get(new ScoreBoardRequest());
            var leftItems = result.ItemsLeft.ToList().First();
            leftItems.MenScores.ShouldHaveCount(1);
            leftItems.WomenScores.ShouldHaveCount(1);
            var rightItems = result.ItemsRight.ToList().First();
            rightItems.MenScores.ShouldHaveCount(1);
            rightItems.WomenScores.ShouldHaveCount(1);
        }
    }
}