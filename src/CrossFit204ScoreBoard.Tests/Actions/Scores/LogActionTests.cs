using CrossFit204ScoreBoard.Web.Actions;
using CrossFit204ScoreBoard.Web.Actions.Scores;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using CrossFit204ScoreBoard.Web.Services;
using FubuMVC.Core.Continuations;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Actions.Scores
{
    [TestFixture]
    public class LogActionGetTests : InteractionContext<LogAction>
    {
        private Workout workout = new Workout();
        private const string AthleteId = "AthleteId";
        private const string WorkoutId = "WorkoutId";
        private LogScoreViewModel result;

        protected override void beforeEach()
        {
            Services
                .Get<IDocumentSession>()
                .Stub(s => s.Load<Workout>(WorkoutId))
                .Return(workout);
            Services
                .Get<IUserContext>()
                .Stub(c => c.User)
                .Return(new Athlete { Id = AthleteId });
            result = ClassUnderTest.Get(new LogScoreRequest { WorkoutId = WorkoutId });
        }

        [Test]
        public void ShouldGetAthleteId()
        {
            result.AthleteId.ShouldEqual(AthleteId);
        }

        [Test]
        public void ShouldGetWorkout()
        {
            result.Workout.ShouldEqual(workout);
        }

        [Test]
        public void ScoreAndTimeShouldNotBeNull()
        {
            result
                .Score.ShouldBeOfType<Score>()
                .Time.ShouldBeOfType<Time>();
        }
    }

    [TestFixture]
    public class LogActionPostTests : InteractionContext<LogAction>
    {
        private FubuContinuation result;
        private readonly Score score = new Score();
        private const string AthleteId = "AthleteId";
        private const string WorkoutId = "WorkoutId";

        protected override void beforeEach()
        {
            var workout = new Workout {Id = WorkoutId};
            result = ClassUnderTest.Post(new LogScoreViewModel
                                             {
                                                 AthleteId = AthleteId,
                                                 Workout = workout,
                                                 Score = score
                                             });
        }

        [Test]
        public void ShouldUpdateScore()
        {
            Services
                .Get<ITopScoreUpdater>()
                .AssertWasCalled(u => u.Update(WorkoutId, AthleteId, score));

        }

        [Test]
        public void ShouldRedirectToScoreBoard()
        {
            result.AssertWasRedirectedTo<ScoreBoardRequest>(r => true);
        }
    }
}