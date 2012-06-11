namespace CrossFit204ScoreBoard.Web.Actions.Athletes
{
    public class ListAction
    {
        public AthleteListViewModel Get(AthleteListRequest request)
        {
            return new AthleteListViewModel();
        }
         
    }

    public class AthleteListRequest {}

    public class AthleteListViewModel {}
}