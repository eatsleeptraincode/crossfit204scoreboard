using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Scores
{
    public class DeleteScoreAction
    {
        private readonly IDocumentSession session;
        private readonly IUserContext context;

        public DeleteScoreAction(IDocumentSession session, IUserContext context)
        {
            this.session = session;
            this.context = context;
        }

        public FubuContinuation Get(DeleteScoreRequest request)
        {
            var score = session.Load<Score>();
            session.Delete(score);
            var athleteId = context.User.Id;
            return  FubuContinuation.RedirectTo(new AthleteDetailsRequest {AthleteId = athleteId});
        }
    }

    public class DeleteScoreRequest
    {
        [RouteInput]
        public string ScoreId { get; set; }
    }
}