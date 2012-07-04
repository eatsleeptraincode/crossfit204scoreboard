using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Actions.Athletes
{
    [TestFixture]
    public class ListActionTests  : RavenContext<ListAction>
    {
        [Test]
        public void ReturnAllAthletes()
        {
            Session.Store(new Athlete {Gender = Gender.Male});
            Session.Store(new Athlete {Gender = Gender.Female});
            Session.Store(new Athlete {Gender = Gender.Female});
            Session.SaveChanges();

            var result = ClassUnderTest.Get(new AthleteListRequest());
            result.FemaleAthletes.ShouldHaveCount(2);
            result.MaleAthletes.ShouldHaveCount(1);
        }
    }
}