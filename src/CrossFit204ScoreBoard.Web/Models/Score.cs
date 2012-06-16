namespace CrossFit204ScoreBoard.Web.Models
{
    public class Score
    {
        public Score()
        {
            Time = new Time();
        }

        public Athlete Athlete { get; set; }

        public decimal Weight { get; set; }
        public int Reps { get; set; }
        public int Rounds { get; set; }
        public Time Time { get; set; }
        public Workout Workout { get; set; }

        public bool IsBetterThan(Score currentScore)
        {
            return Time.IsBetterThan(currentScore.Time)
                   || Weight > currentScore.Weight
                   || Rounds > currentScore.Rounds
                   || Reps > currentScore.Reps;
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