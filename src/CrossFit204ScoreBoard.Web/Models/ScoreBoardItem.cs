using System.Collections.Generic;
using System.Linq;

namespace CrossFit204ScoreBoard.Web.Models
{
    public class ScoreBoardItem
    {
        public Workout Workout { get; private set; }
        private readonly IEnumerable<ScoreDisplay> scores;

        public ScoreBoardItem(Workout workout, IEnumerable<ScoreDisplay> scores)
        {
            Workout = workout;
            this.scores = scores;
        }

        public IEnumerable<ScoreDisplay> MenScores
        {
            get { return scores.Where(s => s.Athlete.Gender == Gender.Male).Order(); }
        }

        public IEnumerable<ScoreDisplay> WomenScores
        {
            get { return scores.Where(s => s.Athlete.Gender == Gender.Female).Order(); }
        }
    }
}