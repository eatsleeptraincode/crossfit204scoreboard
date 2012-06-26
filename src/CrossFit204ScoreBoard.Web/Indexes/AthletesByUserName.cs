using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Client.Indexes;

namespace CrossFit204ScoreBoard.Web.Indexes
{
    public class AthletesByUserName :AbstractIndexCreationTask<Athlete>
    {
        public AthletesByUserName()
        {
            Map = athletes => from a in athletes
                              select new {a.UserName};
        }
    }
}