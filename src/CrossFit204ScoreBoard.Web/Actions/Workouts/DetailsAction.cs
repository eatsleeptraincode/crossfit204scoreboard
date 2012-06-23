using CrossFit204ScoreBoard.Web.Indexes;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core;
using Raven.Client;
using Raven.Client.Linq;

namespace CrossFit204ScoreBoard.Web.Actions.Workouts
{
    public class DetailsAction
    {
        private readonly IDocumentSession session;

        public DetailsAction(IDocumentSession session)
        {
            this.session = session;
        }

        public WorkoutDetailsViewModel Get(WorkoutDetailsRequest request)
        {
            var workout = session.Load<Workout>(request.WorkoutId);
            var scores = session
                            .Query<Score, ScoreBoardIndex>()
                            .Where(s => s.WorkoutId == request.WorkoutId)
                            .As<ScoreDisplay>();
            return new WorkoutDetailsViewModel { Item = new ScoreBoardItem(workout, scores) };
        }
    }

    public class WorkoutDetailsRequest
    {
        [RouteInput]
        public string WorkoutId { get; set; }
    }

    public class WorkoutDetailsViewModel
    {
        public ScoreBoardItem Item { get; set; }
    }
}