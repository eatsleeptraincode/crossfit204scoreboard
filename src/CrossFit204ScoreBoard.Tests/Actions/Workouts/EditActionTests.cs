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
    public class EditActionGetTests : InteractionContext<EditAction>
    {
        [Test]
        public void ShouldReturnWorkout()
        {
            const string workoutId = "WorkoutId";
            var workout = new Workout();
            Services.Get<IDocumentSession>().Stub(s => s.Load<Workout>(workoutId)).Return(workout);
            var result = ClassUnderTest.Get(new EditWorkoutRequest{WorkoutId = workoutId});
            result.Workout.ShouldEqual(workout);
        }
    }

    [TestFixture]
    public class EditActionPostTests : InteractionContext<EditAction>
    {
        FubuContinuation result;
        private readonly Workout workout = new Workout();

        protected override void beforeEach()
        {
            result = ClassUnderTest.Post(new EditWorkoutViewModel{Workout = workout});
        }

        [Test]
        public void ShouldStoreWorkoutFromRequest()
        {
            Services.Get<IDocumentSession>().AssertWasCalled(s => s.Store(workout));
        }

        [Test]
        public void ShouldRedirectToWorkoutList()
        {
            result.AssertWasRedirectedTo<WorkoutListRequest>(r => true);
        }
    }
}