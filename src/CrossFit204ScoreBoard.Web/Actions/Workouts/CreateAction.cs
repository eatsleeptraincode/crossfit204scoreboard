using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.Continuations;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Workouts
{
    public class CreateAction
    {
        readonly IDocumentSession session;
        
        public CreateAction(IDocumentSession session)
        {
            this.session = session;
        }

        public CreateWorkoutViewModel Get(CreateWorkoutRequest request)
        {
            return new CreateWorkoutViewModel();
        }

        public FubuContinuation Post(CreateWorkoutViewModel request)
        {
            session.Store(request.Workout);
            session.SaveChanges();
            return FubuContinuation.RedirectTo(new WorkoutListRequest());
        }
    }

    public class CreateWorkoutRequest {}

    public class CreateWorkoutViewModel
    {
        public Workout Workout { get; set; }
    }
}