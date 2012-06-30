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
            var items = session
                .Query<Score, ScoreBoardIndex>()
                .Customize(c => c.RandomOrdering())
                .As<ScoreDisplay>()
                .ToList()
                .GroupBy(s => s.Workout.Id)
                .Select(score => new ScoreBoardItem(score.First().Workout, score))
                .ToList();

            var half = items.Count() / 2;
            return new ScoreBoardViewModel { ItemsLeft = items.Take(half), ItemsRight = items.Skip(half) };
        }
    }

    public class ScoreBoardRequest {}

    public class ScoreBoardViewModel
    {
        public IEnumerable<ScoreBoardItem> ItemsLeft { get; set; }
        public IEnumerable<ScoreBoardItem> ItemsRight { get; set; }
    }
}