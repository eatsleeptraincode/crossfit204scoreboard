using System.Collections.Generic;
using CrossFit204ScoreBoard.Web.Models;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Models
{
    [TestFixture]
    public class ScoreBoardItemTests
    {
         [Test]
        public void ShouldSplitScoresByGender()
         {
             var item = new ScoreBoardItem(new Workout(),
                                           new List<ScoreDisplay>
                                               {
                                                   new ScoreDisplay {Athlete = new Athlete {Gender = Gender.Male}},
                                                   new ScoreDisplay {Athlete = new Athlete {Gender = Gender.Male}},
                                                   new ScoreDisplay {Athlete = new Athlete {Gender = Gender.Female}}
                                               });
             item.MenScores.ShouldHaveCount(2);
             item.WomenScores.ShouldHaveCount(1);
         }
    }
}