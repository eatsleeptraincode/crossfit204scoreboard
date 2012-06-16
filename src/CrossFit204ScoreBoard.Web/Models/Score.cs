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

        public string WorkoutName { get; set; }
    }

    public class Time
    {
        public decimal Minutes { get; set; }
        public decimal Seconds { get; set; }
    }
}