using System;
using System.Web;
using CrossFit204ScoreBoard.Web.Actions;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using StructureMap;

namespace CrossFit204ScoreBoard.Web
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            FubuApplication
                .For<ConfigureFubuMvc>()
                .StructureMap(() => new Container())
                .Bootstrap();
        }
    }

    public class ConfigureFubuMvc : FubuRegistry
    {
        public ConfigureFubuMvc()
        {
            Applies.ToThisAssembly();

            Actions.IncludeTypesNamed(n => n.EndsWith("Action"));

            Routes
                .HomeIs<ScoreBoardReqeust>()
                .IgnoreNamespaceForUrlFrom<UrlRoot>()
                .IgnoreClassSuffix("Action")
                .IgnoreMethodsNamed("Get")
                .IgnoreMethodsNamed("Post")
                .ConstrainToHttpMethod(c => c.Method.Name.Equals("Get"), "GET")
                .ConstrainToHttpMethod(c => c.Method.Name.Equals("Post"), "POST");

            Views.TryToAttachWithDefaultConventions();
        }
    }
}