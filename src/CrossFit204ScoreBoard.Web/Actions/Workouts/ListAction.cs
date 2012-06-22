using System.Collections.Generic;
using System.Linq;
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
            var workouts = session.Query<Workout>().OrderBy(w => w.Name);
            var half = workouts.Count() / 2;
            return new WorkoutListViewModel { WorkoutsA = workouts.Take(half), WorkoutsB = workouts.Skip(half)};
        }
    }

    public class WorkoutListRequest {}

    public class WorkoutListViewModel
    {
        public IEnumerable<Workout> WorkoutsA { get; set; }
        public IEnumerable<Workout> WorkoutsB { get; set; }
    }
}