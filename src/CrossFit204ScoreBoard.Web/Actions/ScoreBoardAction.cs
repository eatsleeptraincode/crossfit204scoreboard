using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions
{
    public class ScoreBoardAction
    {
        private readonly IDocumentSession session;

        public ScoreBoardAction(IDocumentSession session)
        {
            this.session = session;
        }

        public ScoreBoardViewModel Get(ScoreBoardRequest request)
        {
            var workouts = session.Query<Score>().ToList().GroupBy(s => s.WorkoutName);

            var items = new List<ScoreBoardItem>();
        
            foreach (var workout in workouts)
                items.Add(new ScoreBoardItem(workout.Key, workout.ToList()));

            return new ScoreBoardViewModel {Items = items};
        }
    }

    public class ScoreBoardRequest {}

    public class ScoreBoardViewModel {
        public IEnumerable<ScoreBoardItem> Items { get; set; }
    }
}