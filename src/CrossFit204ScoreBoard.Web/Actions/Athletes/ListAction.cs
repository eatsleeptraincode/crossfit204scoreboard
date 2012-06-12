using System.Collections.Generic;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Athletes
{
    public class ListAction
    {
        readonly IDocumentSession session;
        public ListAction(IDocumentSession session)
        {
            this.session = session;
        }

        public AthleteListViewModel Get(AthleteListRequest request)
        {
            var athletes = session.Query<Athlete>();
            return new AthleteListViewModel{Athletes = athletes};
        }
    }

    public class AthleteListRequest {}

    public class AthleteListViewModel
    {
        public IEnumerable<Athlete> Athletes { get; set; }
    }
}