namespace CrossFit204ScoreBoard.Web.Actions.Workouts
{
    public class ListAction
    {
        public WorkoutListViewModel Get(WorkoutListRequest request)
        {
            return new WorkoutListViewModel();
        }
         
    }

    public class WorkoutListRequest {}

    public class WorkoutListViewModel {}
}