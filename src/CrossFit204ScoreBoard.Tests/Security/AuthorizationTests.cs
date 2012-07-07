using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Actions.Athletes;
using CrossFit204ScoreBoard.Web.Actions.Scores;
using CrossFit204ScoreBoard.Web.Actions.Workouts;
using CrossFit204ScoreBoard.Web.Config;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Registration.Nodes;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Security
{
    [TestFixture]
    public class AuthorizationTests
    {
        IEnumerable<BehaviorChain> chains;

        [TestFixtureSetUp]
        public void Init()
        {
            var graph = new ConfigureFubuMvc().BuildGraph();
            chains = graph.Behaviors.Where(chain => chain.InputType() != null);
        }

        [Test]
        public void ShouldAttachAdminPolicyToCreateWorkout()
        {
            var behaviorChain = chains.Single(chain => chain.InputType() == typeof(CreateWorkoutRequest));
            behaviorChain.Authorization.AllRules.ShouldContain(o => o.Type == typeof(AdminPolicy));
        }

        [Test]
        public void ShouldAttachAdminPolicyToEditWorkout()
        {
            var behaviorChain = chains.Single(chain => chain.InputType() == typeof(EditWorkoutRequest));
            behaviorChain.Authorization.AllRules.ShouldContain(o => o.Type == typeof(AdminPolicy));
        }

        [Test]
        public void ShouldAttachAuthenticationPolicyToLogScore()
        {
            var behaviorChain = chains.Single(chain => chain.InputType() == typeof(LogScoreRequest));
            behaviorChain.Authorization.AllRules.ShouldContain(o => o.Type == typeof(AuthenticationPolicy));
        }

        [Test]
        public void ShouldAttachAuthenticationPolicyToChangePassword()
        {
            var behaviorChain = chains.Single(chain => chain.InputType() == typeof(ChangePasswordRequest));
            behaviorChain.Authorization.AllRules.ShouldContain(o => o.Type == typeof(AuthenticationPolicy));
        }

        [Test]
        public void ShouldAttachUserPolicyToEditAthlete()
        {
            var behaviorChain = chains.Single(chain => chain.InputType() == typeof(EditAthleteRequest));
            behaviorChain.Authorization.AllRules.ShouldContain(o => o.Type == typeof(UserPolicy));
        }

        [Test]
        public void ShouldAttachUserPolicyToDeleteScore()
        {
            var behaviorChain = chains.Single(chain => chain.InputType() == typeof(DeleteScoreRequest));
            behaviorChain.Authorization.AllRules.ShouldContain(o => o.Type == typeof(UserPolicy));
        }
    }
}