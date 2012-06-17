namespace CrossFit204ScoreBoard.Web.Models
{
    public class Workout : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool TrackTime { get; set; }
        public bool TrackWeight { get; set; }
        public bool TrackReps { get; set; }
        public bool TrackRounds { get; set; }
    }
}