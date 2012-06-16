using CrossFit204ScoreBoard.Web.Actions.Workouts;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Continuations;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Actions.Workouts
{
    [TestFixture]
    public class CreateActionGetRequest : InteractionContext<CreateAction>
    {
        [Test]
        public void ShouldReturnNewWorkout()
        {
            var result = ClassUnderTest.Get(new CreateWorkoutRequest());
            result.Workout.ShouldBeOfType<Workout>();
        }
    }

    [TestFixture]
    public class CreateActionPostRequest : InteractionContext<CreateAction>
    {
        Workout workout = new Workout();
        private FubuContinuation result;

        protected override void beforeEach()
        {
            result = ClassUnderTest.Post(new CreateWorkoutViewModel { Workout = workout });
        }

        [Test]
        public void ShouldSaveWorkoutFromRequest()
        {
            Services
                .Get<IDocumentSession>()
                .AssertWasCalled(s => s.Store(workout));
        }

        [Test]
        public void ShouldRedirectToWorkoutList()
        {
            result.AssertWasRedirectedTo<WorkoutListRequest>(r => true);
        }
    }
}