using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core;
using FubuMVC.Core.Continuations;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Workouts
{
    public class EditAction
    {
        private readonly IDocumentSession session;

        public EditAction(IDocumentSession session)
        {
            this.session = session;
        }

        public EditWorkoutViewModel Get(EditWorkoutRequest request)
        {
            var workout = session.Load<Workout>(request.WorkoutId);
            return new EditWorkoutViewModel {Workout = workout};
        }

        public FubuContinuation Post(EditWorkoutViewModel request)
        {
            session.Store(request.Workout);
            return FubuContinuation.RedirectTo(new WorkoutListRequest());
        }
    }

    public class EditWorkoutRequest
    {
        [RouteInput]
        public string WorkoutId { get; set; }
    }

    public class EditWorkoutViewModel
    {
        public Workout Workout { get; set; }
    }
}