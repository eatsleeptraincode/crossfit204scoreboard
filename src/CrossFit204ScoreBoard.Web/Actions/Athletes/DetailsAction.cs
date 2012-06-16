using System.Collections.Generic;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core;
using Raven.Client;

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
            var workouts = session.Query<Workout>();
            return new AthleteDetailsViewModel { Athlete = athlete, Workouts = workouts };
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
        public IEnumerable<Workout> Workouts { get; set; }
    }
}