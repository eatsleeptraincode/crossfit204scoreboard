using System.Collections.Generic;
using CrossFit204ScoreBoard.Web.Indexes;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Actions.Search
{
    public class ResultsAction
    {
        private readonly IDocumentSession session;

        public ResultsAction(IDocumentSession session)
        {
            this.session = session;
        }

        public SearchResultsViewModel Get(SearchResultsRequest request)
        {
            var athletes = session
                .Advanced
                .LuceneQuery<Athlete, AthletesByName>()
                .Search(a => a.FirstName, request.Term)
                .OrElse()
                .Search(a => a.LastName, request.Term);

            var workouts = session
                .Advanced
                .LuceneQuery<Workout, WorkoutsByName>()
                .Search(w => w.Name, request.Term)
                .OrElse()
                .Search(w => w.Description, request.Term);

            return new SearchResultsViewModel{Workouts = workouts, Athletes = athletes};
        }
    }

    public class SearchResultsRequest
    {
        public string Term { get; set; }
    }

    public class SearchResultsViewModel
    {
        public IEnumerable<Athlete> Athletes { get; set; }
        public IEnumerable<Workout> Workouts { get; set; } 
    }
}