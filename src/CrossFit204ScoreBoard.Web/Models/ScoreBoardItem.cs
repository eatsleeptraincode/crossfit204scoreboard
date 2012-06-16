using System.Collections.Generic;
using System.Linq;

namespace CrossFit204ScoreBoard.Web.Models
{
    public class ScoreBoardItem
    {
        public Workout Workout { get; private set; }
        private readonly List<Score> scores;

        public ScoreBoardItem(Workout workout, List<Score> scores)
        {
            Workout = workout;
            this.scores = scores;
        }

        public IEnumerable<Score> MenScores
        {
            get { return scores.Where(s => s.Athlete.Gender == Gender.Male); }
        }

        public IEnumerable<Score> WomenScores
        {
            get { return scores.Where(s => s.Athlete.Gender == Gender.Female); }
        }
    }
}