using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Client;
using Raven.Client.Linq;

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
            return new AthleteListViewModel
                       {
                           MaleAthletes = athletes.Where(a => a.Gender == Gender.Male).ToList().OrderBy(a => a.FullName),
                           FemaleAthletes = athletes.Where(a => a.Gender == Gender.Female).ToList().OrderBy(a => a.FullName)
                       };
        }
    }

    public class AthleteListRequest {}

    public class AthleteListViewModel
    {

        public IEnumerable<Athlete> MaleAthletes { get; set; }
        public IEnumerable<Athlete> FemaleAthletes { get; set; }
    }
}