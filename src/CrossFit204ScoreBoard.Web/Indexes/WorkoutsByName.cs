using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace CrossFit204ScoreBoard.Web.Indexes
{
    public class WorkoutsByName : AbstractIndexCreationTask<Workout>
    {
        public WorkoutsByName()
        {
            Map = workouts => from w in workouts
                              select new {w.Id, w.Name, w.Description};

            Indexes.Add(w => w.Name, FieldIndexing.Analyzed);
            Indexes.Add(w => w.Description, FieldIndexing.Analyzed);
        }
    }
}