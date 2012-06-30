using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Client.Indexes;

namespace CrossFit204ScoreBoard.Web.Indexes
{
    public class ScoreBoardIndex : AbstractIndexCreationTask<Score>
    {
        public ScoreBoardIndex()
        {
            Map =
                scores => from score in scores
                          select new
                                     {
                                         score.WorkoutId,
                                         score.AthleteId,
                                         score.Weight,
                                         score.Time,
                                         score.Reps,
                                         score.Rounds
                                     };

            TransformResults =
                (database, scores) => from score in scores
                                      let workout = database.Load<Workout>(score.WorkoutId)
                                      let athlete = database.Load<Athlete>(score.AthleteId)
                                      select new
                                                 {
                                                     Workout = workout,
                                                     Athlete = athlete,
                                                     score.Id,
                                                     score.WorkoutId,
                                                     score.AthleteId,
                                                     score.Weight,
                                                     score.Time,
                                                     score.Reps,
                                                     score.Rounds
                                                 };
        }
    }
}