using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace CrossFit204ScoreBoard.Web.Indexes
{
    public class AthletesByName : AbstractIndexCreationTask<Athlete>
    {
        public AthletesByName()
        {
            Map = athletes => from a in athletes
                              select new {a.Id, a.FirstName, a.LastName};

            Indexes.Add(a => a.FirstName, FieldIndexing.Analyzed);
            Indexes.Add(a => a.LastName, FieldIndexing.Analyzed);
        }
    }
}