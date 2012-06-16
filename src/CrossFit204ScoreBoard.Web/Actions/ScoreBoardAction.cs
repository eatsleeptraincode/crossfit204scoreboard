using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Indexes;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Client;
using Raven.Client.Linq;

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
            var workouts = session.Query<Score, ScoreBoardIndex>().As<ScoreDisplay>().ToList().GroupBy(s => s.Workout);

            var items = new List<ScoreBoardItem>();
        
            foreach (var workout in workouts)
                items.Add(new ScoreBoardItem(workout.Key, workout.ToList()));

            return new ScoreBoardViewModel {Items = items};
        }
    }

    public class ScoreBoardRequest {}

    public class ScoreBoardViewModel
    {
        public IEnumerable<ScoreBoardItem> Items { get; set; }
    }
}