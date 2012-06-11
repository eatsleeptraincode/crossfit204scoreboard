using System.Collections.Generic;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Workouts
{
    public class ListAction
    {
        readonly IDocumentSession session;

        public ListAction(IDocumentSession session)
        {
            this.session = session;
        }

        public WorkoutListViewModel Get(WorkoutListRequest request)
        {
            var workouts = session.Query<Workout>();
            return new WorkoutListViewModel {Workouts = workouts};
        }
    }

    public class WorkoutListRequest {}

    public class WorkoutListViewModel
    {
        public IEnumerable<Workout> Workouts { get; set; }
    }
}