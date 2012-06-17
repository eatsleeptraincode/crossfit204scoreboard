using System.Linq;
using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Models;
using Raven.Client;

namespace CrossFit204ScoreBoard.Web.Validation.Rules
{
    public class UserNameIsUnique : ModelRule<RegisterViewModel>
    {
        private readonly IDocumentSession session;

        public UserNameIsUnique(IDocumentSession session)
        {
            this.session = session;
        }

        protected override void Validate()
        {
            var userName = GetValue(m => m.Athlete.UserName);
            var existingAthlete = session
                                    .Query<Athlete>()
                                    .SingleOrDefault(a => a.UserName == userName);
            if (existingAthlete != null)
                RegisterError("UserIsNotUnique","Some one has already registered with that user name",Accessor(m => m.Athlete.UserName));
        }
    }
}