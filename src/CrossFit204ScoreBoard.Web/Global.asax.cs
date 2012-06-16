using System;
using System.Web;
using Bottles;
using CrossFit204ScoreBoard.Web.Config;
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
                .StructureMap(() => new Container(x => x.IncludeRegistry<ConfigureStructureMap>()))
                .Bootstrap();

            PackageRegistry.AssertNoFailures();
        }
    }
}