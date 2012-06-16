using CrossFit204ScoreBoard.Web.Actions.Workouts;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Actions.Workouts
{
    [TestFixture]
    public class ListActionGetRequest : InteractionContext<ListAction>
    {
        [Test,Ignore]
        public void ShouldReturnAllWorkouts()
        {
            var result = ClassUnderTest.Get(new WorkoutListRequest());
            result.Workouts.ShouldHaveCount(2);
        }
    }
}