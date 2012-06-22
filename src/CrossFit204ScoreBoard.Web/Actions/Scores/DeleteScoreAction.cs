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

        public DeleteScoreAction(IDocumentSession session)
        {
            this.session = session;
        }

        public FubuContinuation Get(DeleteScoreRequest request)
        {
            var score = session.Load<Score>("scores/" + request.ScoreId);
            session.Delete(score);
            var athleteId = FubuPrincipal.Current.User.Id.Replace("athletes/", "");
            return  FubuContinuation.RedirectTo(new AthleteDetailsRequest {AthleteId = athleteId});
        }
    }

    public class DeleteScoreRequest
    {
        [RouteInput]
        public string ScoreId { get; set; }
    }
}