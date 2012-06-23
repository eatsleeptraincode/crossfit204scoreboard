using System.Collections.Generic;
using CrossFit204ScoreBoard.Web.Indexes;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core;
using Raven.Client;
using Raven.Client.Linq;

namespace CrossFit204ScoreBoard.Web.Actions.Athletes
{
    public class DetailsAction
    {
        private readonly IDocumentSession session;

        public DetailsAction(IDocumentSession session)
        {
            this.session = session;
        }

        public AthleteDetailsViewModel Get(AthleteDetailsRequest request)
        {
            var athleteId = "athletes/" + request.AthleteId;
            var athlete = session.Load<Athlete>(athleteId);
            var scores = session
                .Query<Score, ScoreBoardIndex>()
                .Where(s => s.AthleteId == athleteId)
                .As<ScoreDisplay>();
            return new AthleteDetailsViewModel { Athlete = athlete, Scores = scores };
        }
    }

    public class AthleteDetailsRequest
    {
        [RouteInput]
        public string AthleteId { get; set; }
    }

    public class AthleteDetailsViewModel
    {
        public Athlete Athlete { get; set; }
        public IEnumerable<ScoreDisplay> Scores { get; set; }
    }
}