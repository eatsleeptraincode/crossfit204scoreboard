using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Athletes
{
    public class EditAction
    {
        private readonly IDocumentSession session;

        public EditAction(IDocumentSession session)
        {
            this.session = session;
        }

        public EditAthleteViewModel Get(EditAthleteRequest request)
        {
            var athlete = session.Load<Athlete>("athletes/" + request.AthleteId);
            return new EditAthleteViewModel {Athlete = athlete};
        }

        public FubuContinuation Post(EditAthleteViewModel request)
        {
            var athlete = session.Load<Athlete>(request.Athlete.Id);
            athlete.FirstName = request.Athlete.FirstName;
            athlete.LastName = request.Athlete.LastName;
            athlete.Gender = request.Athlete.Gender;
            return FubuContinuation.RedirectTo(new AthleteDetailsRequest {AthleteId = request.Athlete.Id.Replace("athletes/","")});
        }
         
    }

    public class EditAthleteRequest
    {
        [RouteInput]
        public string AthleteId { get; set; }
    }

    public class EditAthleteViewModel
    {
        public Athlete Athlete { get; set; }
    }
}