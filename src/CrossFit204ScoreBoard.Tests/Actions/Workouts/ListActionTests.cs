using CrossFit204ScoreBoard.Web.Actions.Workouts;
using CrossFit204ScoreBoard.Web.Models;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Actions.Workouts
{
    [TestFixture]
    public class ListActionGetRequest : RavenContextWithIndex<ListAction>
    {
        Workout workoutA = new Workout { Name = "A" };
        Workout workoutB = new Workout { Name = "B" };

        protected override void BeforeIndexing()
        {
            Session.Store(workoutA);
            Session.Store(workoutB);
        }

        [Test]
        public void ShouldReturnAllWorkouts()
        {
            var result = ClassUnderTest.Get(new WorkoutListRequest());
            result.WorkoutsA.ShouldContain(workoutA);
            result.WorkoutsB.ShouldContain(workoutB);
        }
    }
}