using System.Collections.Generic;
using System.Linq;

namespace CrossFit204ScoreBoard.Web.Models
{
    public class Score
    {
        public Score()
        {
            Time = new Time();
        }

        public string Id { get; set; }
        public string AthleteId { get; set; }
        public string WorkoutId { get; set; }

        public decimal Weight { get; set; }
        public int Reps { get; set; }
        public int Rounds { get; set; }
        public Time Time { get; set; }

        public bool IsBetterThan(Score currentScore)
        {
            return Time.IsBetterThan(currentScore.Time)
                   || Weight > currentScore.Weight
                   || Rounds > currentScore.Rounds
                   || Rounds == currentScore.Rounds && Reps > currentScore.Reps;
        }
    }

    public class ScoreDisplay : Score
    {
        public Athlete Athlete { get; set; }
        public Workout Workout { get; set; }
    }

    public static class ScoreOrdering
    {
        public static IOrderedEnumerable<ScoreDisplay> Order(this IEnumerable<ScoreDisplay> scores)
        {
            return scores
                .OrderBy(s => s.Time.Minutes)
                .ThenBy(s => s.Time.Seconds)
                .ThenByDescending(s => s.Weight)
                .ThenByDescending(s => s.Rounds)
                .ThenByDescending(s => s.Reps);
        }
    }



    public class Time
    {
        public decimal Minutes { get; set; }
        public decimal Seconds { get; set; }

        public bool IsBetterThan(Time time)
        {
            return Minutes < time.Minutes
                || Minutes == time.Minutes && Seconds < time.Seconds;
        }
    }
}