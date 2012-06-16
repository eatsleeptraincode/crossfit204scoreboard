using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Indexes;

namespace CrossFit204ScoreBoard.Web.Models
{
    public class ScoreBoardItem
    {
        public Workout Workout { get; private set; }
        private readonly List<ScoreDisplay> scores;

        public ScoreBoardItem(Workout workout, List<ScoreDisplay> scores)
        {
            Workout = workout;
            this.scores = scores;
        }

        public IEnumerable<ScoreDisplay> MenScores
        {
            get { return scores.Where(s => s.Athlete.Gender == Gender.Male); }
        }

        public IEnumerable<ScoreDisplay> WomenScores
        {
            get { return scores.Where(s => s.Athlete.Gender == Gender.Female); }
        }
    }
}