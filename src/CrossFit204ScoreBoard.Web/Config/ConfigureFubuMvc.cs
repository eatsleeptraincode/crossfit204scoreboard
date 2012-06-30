using System;
using System.Linq;
using CrossFit204ScoreBoard.Web.Actions;
using CrossFit204ScoreBoard.Web.Behaviours;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Security.AntiForgery;
using FubuMVC.Validation;
using FubuValidation;

namespace CrossFit204ScoreBoard.Web.Config
{
    public class ConfigureFubuMvc : FubuRegistry
    {
        public ConfigureFubuMvc()
        {
            Applies.ToThisAssembly();

            Actions.IncludeTypesNamed(n => n.EndsWith("Action"));

            Routes
                .HomeIs<ScoreBoardRequest>()
                .IgnoreNamespaceForUrlFrom<UrlRoot>()
                .IgnoreClassSuffix("Action")
                .IgnoreMethodsNamed("Get")
                .IgnoreMethodsNamed("Post")
                .ConstrainToHttpMethod(c => c.Method.Name.Equals("Get"), "GET")
                .ConstrainToHttpMethod(c => c.Method.Name.Equals("Post"), "POST");

            Views
                .TryToAttachWithDefaultConventions()
                .RegisterActionLessViews(t => t.ViewModel == typeof(Notification));

            HtmlConvention<ConfigureHtmlConventions>();
            StringConversions(c => c.IfIsType<decimal>().ConvertBy(d => d.ToString("##.#")));

            Policies
                .Add<AntiForgeryPolicy>()
                .Add<AttachAuthenticationPolicy>()
                .Add<AttachUserPolicy>()
                .Add<AttachAdminPolicy>()
                .WrapBehaviorChainsWith<RavenTransactionBehaviour>()
                .WrapBehaviorChainsWith<load_the_current_principal>();

            this.Validation(v => { v.Actions.Include(a => a.HasInput && a.InputType().Name.EndsWith("ViewModel"));
            v.Failures.If(a => a.InputType() != null && a.InputType().Name.EndsWith("ViewModel")).TransferBy<ViewModelDescriptor>();});
        }
    }

    public class ViewModelDescriptor : IFubuContinuationModelDescriptor
    {
        private readonly BehaviorGraph graph;

        public ViewModelDescriptor(BehaviorGraph graph)
        {
            this.graph = graph;
        }

        public Type DescribeModelFor(ValidationFailure context)
        {
            var handlerType = context.Target.HandlerType;

            var getCall = graph
                .Behaviors
                .Where(chain => chain.FirstCall() != null
                    && chain.FirstCall().HandlerType == handlerType
                    && chain.Route.AllowedHttpMethods.Contains("GET"))
                .Select(chain => chain.FirstCall())
                .FirstOrDefault();

            if(getCall == null)
                return null;

            return getCall.InputType();
        }
    }
}