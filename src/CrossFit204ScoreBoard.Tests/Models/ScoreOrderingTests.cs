using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Models
{
    [TestFixture]
    public class ScoreOrderingTests
    {
        [Test]
        public void ShouldOrderScoresByTime()
        {
            var bestTimeScore = new ScoreDisplay {Time = new Time {Minutes = 1, Seconds = 2.3M}};
            var middleTimeScore = new ScoreDisplay {Time = new Time {Minutes = 1, Seconds = 34.5M}};
            var worstTimeScore = new ScoreDisplay {Time = new Time {Minutes = 12, Seconds = 34.5M}};

            var scores = new List<ScoreDisplay>
                             {
                                 middleTimeScore,
                                 worstTimeScore,
                                 bestTimeScore,
                             };

            var orderedScores = scores.Order().ToList();
            orderedScores[0].ShouldEqual(bestTimeScore);
            orderedScores[1].ShouldEqual(middleTimeScore);
            orderedScores[2].ShouldEqual(worstTimeScore);
        }

        [Test]
        public void ShouldOrderScoresByWeight()
        {
            var bestWeightScore = new ScoreDisplay { Weight = 123M };
            var worstWeightScore = new ScoreDisplay { Weight = 100M };

            var scores = new List<ScoreDisplay>
                             {
                                 worstWeightScore,
                                 bestWeightScore
                             };

            var orderedScores = scores.Order().ToList();
            orderedScores[0].ShouldEqual(bestWeightScore);
            orderedScores[1].ShouldEqual(worstWeightScore);
        }

        [Test]
        public void ShouldOrderScoresByRounds()
        {
            var bestRoundScore = new ScoreDisplay { Rounds = 10 };
            var worstRoundScore = new ScoreDisplay { Rounds = 1 };

            var scores = new List<ScoreDisplay>
                             {
                                 worstRoundScore,
                                 bestRoundScore,
                             };

            var orderedScores = scores.Order().ToList();
            orderedScores[0].ShouldEqual(bestRoundScore);
            orderedScores[1].ShouldEqual(worstRoundScore);
        }

        [Test]
        public void ShouldOrderScoresByReps()
        {
            var bestRepScore = new ScoreDisplay { Reps = 10 };
            var worstRepScore = new ScoreDisplay { Reps = 1 };

            var scores = new List<ScoreDisplay>
                             {
                                 worstRepScore,
                                 bestRepScore,
                             };

            var orderedScores = scores.Order().ToList();
            orderedScores[0].ShouldEqual(bestRepScore);
            orderedScores[1].ShouldEqual(worstRepScore);
        }
    }
}