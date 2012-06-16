namespace CrossFit204ScoreBoard.Web.Models
{
    public class Score
    {
        public string AthleteId { get; set; }
        public string WorkoutId { get; set; }
        public decimal Points { get; set; }
        public string AthleteFirstName { get; set; }
        public string AthleteLastName { get; set; }
        public string WorkoutName { get; set; }
    }
}