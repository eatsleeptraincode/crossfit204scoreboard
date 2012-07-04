using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Actions.Workouts;
using FubuMVC.Core.Registration;

namespace CrossFit204ScoreBoard.Web.Security
{
    public class AttachAdminPolicy : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph
                .Behaviors
                .Where(chain => chain.InputType() != null)
                .Where(chain => chain.InputType() == typeof (CreateWorkoutRequest)
                                || chain.InputType() == typeof (EditWorkoutRequest))
                .Each(chain => chain
                                   .Authorization
                                   .AddPolicy(typeof (AdminPolicy)));
        }
    }
}