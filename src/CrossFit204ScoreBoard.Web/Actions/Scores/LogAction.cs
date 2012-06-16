using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Scores
{
    public class LogAction
    {
        private readonly IDocumentSession session;

        public LogAction(IDocumentSession session)
        {
            this.session = session;
        }

        public LogScoreViewModel Get(LogScoreRequest request)
        {
            var athleteId = FubuPrincipal.Current.User.Id;
            var workout = session.Load<Workout>("workouts/" + request.WorkoutId);
            return new LogScoreViewModel { AthleteId = athleteId, Workout = workout, Score = new Score() };
        }

        public FubuContinuation Post(LogScoreViewModel request)
        {
            var athleteId = request.AthleteId;
            var athlete = session.Load<Athlete>(athleteId);
            var workout = session.Load<Workout>(request.Workout.Id);
            var currentScore = session.Query<Score>().SingleOrDefault(s => s.Athlete.Id == athleteId);
            if (currentScore == null)
            {
                Score score = request.Score;
                score.Athlete = athlete;
                score.Workout = workout;
                session.Store(score);
            }
            else if (request.Score.IsBetterThan(currentScore))
            {
                session.Delete(currentScore);
                Score score = request.Score;
                score.Athlete = athlete;
                score.Workout = workout;
                session.Store(score);
            }

            return FubuContinuation.RedirectTo(new ScoreBoardRequest());
        }
    }

    public class LogScoreRequest
    {
        [RouteInput]
        public string WorkoutId { get; set; }
    }

    public class LogScoreViewModel
    {
        public string AthleteId { get; set; }
        public Workout Workout { get; set; }
        public decimal Points { get; set; }
        public Score Score { get; set; }
    }

    public static class Extensions
    {
        public static string RavenId<T>(T entity) where T : Entity
        {
            return typeof (T).Name.ToLower() + "/" + entity.Id;
        }
    }
}