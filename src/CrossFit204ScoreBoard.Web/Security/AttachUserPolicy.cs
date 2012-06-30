using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Actions.Scores;
using FubuMVC.Core.Registration;

namespace CrossFit204ScoreBoard.Web.Security
{
    public class AttachUserPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph
                .Behaviors
                .Where(chain => chain.InputType() != null)
                .Where(chain => chain.InputType() == typeof (EditAthleteRequest)
                                || chain.InputType() == typeof (DeleteScoreRequest))
                .Each(chain => chain
                                   .Authorization
                                   .AddPolicy(typeof (UserPolicy)));
        }
    }
}