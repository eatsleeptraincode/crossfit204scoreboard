using CrossFit204ScoreBoard.Web.Security;
using FubuCore.Configuration;
using FubuMVC.Core.Security;
using FubuMVC.StructureMap;
using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace CrossFit204ScoreBoard.Web.Config
{
    public class ConfigureStructureMap : Registry
    {
        public ConfigureStructureMap()
        {
            Scan(s =>
                     {
                         s.TheCallingAssembly();
                         s.WithDefaultConventions();
                         s.Convention<SettingsScanner>();
                     });

            For<ISettingsProvider>().Use<AppSettingsProvider>();
            SetAllProperties(s => s.Matching(p => p.Name.EndsWith("Settings")));

            ForSingletonOf<IDocumentStore>()
                .Use(new DocumentStore { ConnectionStringName = "RavenDb" }.Initialize());

            For<IDocumentSession>()
                .Use(ctx => ctx.GetInstance<IDocumentStore>().OpenSession());

            For<IPrincipalFactory>()
                .Use<FubuPrincipalFactory>();
        }
    }
}