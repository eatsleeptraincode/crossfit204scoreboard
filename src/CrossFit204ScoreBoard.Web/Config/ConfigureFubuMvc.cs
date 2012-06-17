using CrossFit204ScoreBoard.Web.Actions;
using CrossFit204ScoreBoard.Web.Behaviours;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Security.AntiForgery;
using FubuMVC.Core.UI.Forms;

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

            Views.TryToAttachWithDefaultConventions();

            HtmlConvention<ConfigureHtmlConventions>();

            Policies
                .Add<AntiForgeryPolicy>()
                .Add<AttachAuthenticationPolicy>()
                .Add<AttachUserPolicy>()
                .Add<AttachAdminPolicy>()
                .WrapBehaviorChainsWith<RavenTransactionBehaviour>()
                .WrapBehaviorChainsWith<load_the_current_principal>();
        }
    }
}