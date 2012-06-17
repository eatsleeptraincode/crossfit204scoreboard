using System;
using FubuValidation;

namespace CrossFit204ScoreBoard.Web.Models
{
    public class Athlete : Entity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName{ get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
}