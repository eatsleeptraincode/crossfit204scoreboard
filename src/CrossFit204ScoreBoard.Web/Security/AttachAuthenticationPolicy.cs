using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Actions.Scores;
using FubuMVC.Core.Registration;

namespace CrossFit204ScoreBoard.Web.Security
{
    public class AttachAuthenticationPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph
                .Behaviors
                .Where(chain => chain.InputType() != null)
                .Where(chain => chain.InputType() == typeof (LogScoreRequest)
                                || chain.InputType() == typeof (ChangePasswordRequest))
                .Each(chain => chain
                                   .Authorization
                                   .AddPolicy(typeof (AuthenticationPolicy)));
        }
    }
}