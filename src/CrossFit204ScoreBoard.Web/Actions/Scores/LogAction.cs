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
            var workoutId = request.WorkoutId;
            return new LogScoreViewModel { AthleteId = athleteId, WorkoutId = workoutId };
        }

        public FubuContinuation Post(LogScoreViewModel request)
        {
            var athleteId = request.AthleteId;
            var athlete = session.Load<Athlete>(athleteId);
            var workoutId = "workouts/" + request.WorkoutId;
            var workout = session.Load<Workout>(workoutId);
            var score = new Score
                            {
                                AthleteFirstName = athlete.FirstName,
                                AthleteLastName = athlete.LastName,
                                AthleteId = athlete.Id,
                                WorkoutId = workout.Id,
                                WorkoutName = workout.Name
                            };
            session.Store(score);
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
        public string WorkoutId { get; set; }
        public decimal Points { get; set; }
    }
}