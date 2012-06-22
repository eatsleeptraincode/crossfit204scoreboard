using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using CrossFit204ScoreBoard.Web.Services;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Scores
{
    public class LogAction
    {
        private readonly IDocumentSession session;
        private readonly ITopScoreUpdater updater;

        public LogAction(IDocumentSession session, ITopScoreUpdater updater)
        {
            this.session = session;
            this.updater = updater;
        }

        public LogScoreViewModel Get(LogScoreRequest request)
        {
            var athleteId = FubuPrincipal.Current.User.Id;
            var workout = session.Load<Workout>("workouts/" + request.WorkoutId);
            return new LogScoreViewModel { AthleteId = athleteId, Workout = workout, Score = new Score() };
        }

        public FubuContinuation Post(LogScoreViewModel request)
        {
            updater.Update(request.Workout.Id, request.AthleteId, request.Score);

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