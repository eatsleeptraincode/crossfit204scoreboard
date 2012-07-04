using CrossFit204ScoreBoard.Web.Models;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Models
{
    [TestFixture]
    public class ScoreEvaulationTests
    {
        [Test]
        public void TimeComparisons()
        {
            var score1 = new Score {Time = new Time {Minutes = 12, Seconds = 34.5M}};
            var score2 = new Score {Time = new Time {Minutes = 1, Seconds = 2.3M}};
            var score3 = new Score {Time = new Time {Minutes = 1, Seconds = 2.2M}};
            score1.IsBetterThan(score2).ShouldBeFalse();
            score2.IsBetterThan(score1).ShouldBeTrue();
            score3.IsBetterThan(score2).ShouldBeTrue();
        }

        [Test]
        public void WeightComparison()
        {
            var score1 = new Score {Weight = 123M};
            var score2 = new Score {Weight = 120M};
            score1.IsBetterThan(score2).ShouldBeTrue();
            score2.IsBetterThan(score1).ShouldBeFalse();
        }

        [Test]
        public void RoundComparison()
        {
            var score1 = new Score { Rounds = 12 };
            var score2 = new Score { Rounds = 1 };
            score1.IsBetterThan(score2).ShouldBeTrue();
            score2.IsBetterThan(score1).ShouldBeFalse();
        }

        [Test]
        public void RepComparison()
        {
            var score1 = new Score { Reps = 1 };
            var score2 = new Score { Reps = 12 };
            score1.IsBetterThan(score2).ShouldBeFalse();
            score2.IsBetterThan(score1).ShouldBeTrue();
        }

        [Test]
        public void RoundAndRepComparison()
        {
            var score1 = new Score { Rounds = 12,Reps = 1};
            var score2 = new Score { Rounds = 12, Reps = 12};
            var score3 = new Score { Rounds = 1, Reps = 55};
            score1.IsBetterThan(score2).ShouldBeFalse();
            score2.IsBetterThan(score1).ShouldBeTrue();
            score3.IsBetterThan(score1).ShouldBeFalse();
            score2.IsBetterThan(score3).ShouldBeTrue();
        }
    }
}