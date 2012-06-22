using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.SessionState;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Services
{
    public interface ITopScoreUpdater
    {
        void Update(string workoutId, string athleteId, Score newScore);
    }

    public class TopScoreUpdater : ITopScoreUpdater
    {
        private readonly IDocumentSession session;
        private readonly IFlash flash;

        public TopScoreUpdater(IDocumentSession session, IFlash flash)
        {
            this.session = session;
            this.flash = flash;
        }

        public void Update(string workoutId, string athleteId, Score newScore)
        {
            var currentScore = FindCurrentScore(workoutId, athleteId);

            if (currentScore == null)
                AddNewScore(workoutId, athleteId, newScore);
            else if (newScore.IsBetterThan(currentScore))
                DeleteOldAndAddNewScore(workoutId, athleteId, newScore, currentScore);
        }

        private Score FindCurrentScore(string workoutId, string athleteId)
        {
            var currentScore = session
                .Query<Score>()
                .SingleOrDefault(s => s.AthleteId == athleteId
                                      && s.WorkoutId == workoutId);
            return currentScore;
        }

        private void DeleteOldAndAddNewScore(string workoutId, string athleteId, Score newScore, Score currentScore)
        {
            session.Delete(currentScore);
            AddNewScore(workoutId, athleteId, newScore);
        }

        private void AddNewScore(string workoutId, string athleteId, Score newScore)
        {
            newScore.AthleteId = athleteId;
            newScore.WorkoutId = workoutId;
            session.Store(newScore);
            flash.Flash(new Message {Text = "Congratulations on the new score"});

            var message = flash.Retrieve<Message>();
        }
    }
}