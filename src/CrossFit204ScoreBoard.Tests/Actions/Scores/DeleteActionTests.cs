using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Actions.Scores;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Continuations;
using FubuTestingSupport;
using NUnit.Framework;
using Raven.Client;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Actions.Scores
{
    [TestFixture]
    public class DeleteActionTests : InteractionContext<DeleteAction>
    {
        private const string ScoreId = "ScoreId";
        private FubuContinuation result;
        private readonly Score score = new Score();
        private const string AthleteId = "AthleteId";

        protected override void beforeEach()
        {
            Services.Get<IUserContext>().Stub(c => c.User).Return(new Athlete {Id = AthleteId});
            Services.Get<IDocumentSession>().Stub(s => s.Load<Score>(ScoreId)).Return(score);
            var request = new DeleteScoreRequest {ScoreId = ScoreId};
            result = ClassUnderTest.Get(request);
        }

        [Test]
        public void ShouldDeleteScore()
        {
            Services.Get<IDocumentSession>().AssertWasCalled(s => s.Delete(score));
        }

        [Test]
        public void ShouldRedirectToAthleteDetails()
        {
            result.AssertWasRedirectedTo<AthleteDetailsRequest>(r => r.AthleteId == AthleteId);
        }
    }
}