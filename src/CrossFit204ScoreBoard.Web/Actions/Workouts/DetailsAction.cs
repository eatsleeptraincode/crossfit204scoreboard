using System.Collections.Generic;
using System.Linq;
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
            var workoutId = "workouts/" + request.WorkoutId;
            var scores = session.Query<Score, ScoreBoardIndex>().Where(s => s.WorkoutId == workoutId).As<ScoreDisplay>();
            if (scores.Any())
            {
                return new WorkoutDetailsViewModel { Item = new ScoreBoardItem(scores.First().Workout, scores) };
            }
            else
            {
                Workout workout = session.Load<Workout>(workoutId);
                return new WorkoutDetailsViewModel {Item = new ScoreBoardItem(workout, new List<ScoreDisplay>())};
            }
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