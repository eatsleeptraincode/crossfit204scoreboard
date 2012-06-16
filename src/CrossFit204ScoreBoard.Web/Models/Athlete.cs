namespace CrossFit204ScoreBoard.Web.Models
{
    public class Athlete
    {
        public string Id { get; set; }
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public Gender Gender { get; set; }
        public string UserName{ get; set; }
        public string Password { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}