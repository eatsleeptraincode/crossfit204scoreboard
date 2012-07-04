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
        private readonly IUserContext context;

        public LogAction(IDocumentSession session, ITopScoreUpdater updater, IUserContext context)
        {
            this.session = session;
            this.updater = updater;
            this.context = context;
        }

        public LogScoreViewModel Get(LogScoreRequest request)
        {
            var athleteId = context.User.Id;
            var workout = session.Load<Workout>(request.WorkoutId);
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
        public Score Score { get; set; }
    }
}