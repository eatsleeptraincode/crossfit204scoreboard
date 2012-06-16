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
            var scores = session.Query<Score>();
            return new ScoreBoardViewModel {Scores = scores};
        }
    }

    public class ScoreBoardRequest {}

    public class ScoreBoardViewModel {
        public IQueryable<Score> Scores { get; set; }
    }
}