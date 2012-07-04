using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Services;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Services
{
    [TestFixture]
    public class FirstTimeEnteringScore : RavenContext<TopScoreUpdater>
    {
        [Test]
        public void ShouldAddNewScore()
        {
            const string workoutId = "WorkoutId";
            const string athleteId = "AthleteId";
            var newScore = new Score();
            ClassUnderTest.Update(workoutId, athleteId, newScore);
            Session.Load<Score>(newScore.Id).ShouldEqual(newScore);
        }
    }

    [TestFixture]
    public class WhenNewScoreIsBetter : RavenContextWithIndex<TopScoreUpdater>
    {
        const string WorkoutId = "WorkoutId";
        const string AthleteId = "AthleteId";
        private readonly Score newScore = new Score { Weight = 234M };
        private readonly Score oldScore = new Score { AthleteId = AthleteId, WorkoutId = WorkoutId, Weight = 123M };

        protected override void BeforeIndexing()
        {
            Session.Store(oldScore);
        }

        protected override void Context()
        {
            ClassUnderTest.Update(WorkoutId, AthleteId, newScore);
            Session.SaveChanges();
        }

        [Test]
        public void ShouldAddNewScore()
        {
            Session.Load<Score>(newScore.Id).ShouldEqual(newScore);
        }

        [Test]
        public void ShouldDeleteOldScore()
        {
            Session.Load<Score>(oldScore.Id).ShouldBeNull();
        }
    }

    [TestFixture]
    public class WhenOldScoreIsBetter : RavenContextWithIndex<TopScoreUpdater>
    {
        const string WorkoutId = "WorkoutId";
        const string AthleteId = "AthleteId";
        private readonly Score newScore = new Score {Id = "ScoreId", Weight = 123M};
        private readonly Score oldScore = new Score { AthleteId = AthleteId, WorkoutId = WorkoutId, Weight = 234M };

        protected override void BeforeIndexing()
        {
            Session.Store(oldScore);
        }

        protected override void Context()
        {
            ClassUnderTest.Update(WorkoutId, AthleteId, newScore);
        }

        [Test]
        public void ShouldNotAddNewScore()
        {
            Session.Load<Score>(newScore.Id).ShouldBeNull();
        }

        [Test]
        public void ShouldPreserveOldScore()
        {
            Session.Load<Score>(oldScore.Id).ShouldEqual(oldScore);
        }
    }

}