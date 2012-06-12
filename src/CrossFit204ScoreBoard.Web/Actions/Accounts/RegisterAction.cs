using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Continuations;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Accounts
{
    public class RegisterAction
    {
        readonly IDocumentSession session;

        public RegisterAction(IDocumentSession session)
        {
            this.session = session;
        }

        public RegisterViewModel Get(RegisterRequest request)
        {
            return new RegisterViewModel();
        }

        public FubuContinuation Post(RegisterViewModel request)
        {
            session.Store(request.Athlete);
            return FubuContinuation.RedirectTo(new AthleteListRequest());
        }
    }

    public class RegisterRequest {}

    public class RegisterViewModel
    {
        public Athlete Athlete { get; set; }
        public string ConfirmPassword { get; set; }
    }
}