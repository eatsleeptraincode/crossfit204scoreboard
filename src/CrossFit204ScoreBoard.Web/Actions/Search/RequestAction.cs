namespace CrossFit204ScoreBoard.Web.Actions.Search
{
    public class RequestAction
    {
        public SearchRequestViewModel Get(SearchRequestRequest request)
        {
            return new SearchRequestViewModel();
        }
    }

    public class SearchRequestRequest
    {
    }

    public class SearchRequestViewModel
    {
        public string Term { get; set; }
    }
}