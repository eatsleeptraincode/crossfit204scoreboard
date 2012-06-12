using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core.Security;
using Raven.Client;
using Raven.Client.Document;
using StructureMap.Configuration.DSL;

namespace CrossFit204ScoreBoard.Web.Config
{
    public class ConfigureStructureMap : Registry
    {
        public ConfigureStructureMap()
        {
            ForSingletonOf<IDocumentStore>()
                .Use(new DocumentStore { ConnectionStringName = "RavenDb" }.Initialize());

            For<IDocumentSession>()
                .Use(ctx => ctx.GetInstance<IDocumentStore>().OpenSession());

            For<IPrincipalFactory>()
                .Use<FubuPrincipalFactory>();
        }
    }
}