using CrossFit204ScoreBoard.Web.Security;
using CrossFit204ScoreBoard.Web.Validation;
using FubuCore.Configuration;
using FubuMVC.Core.Security;
using FubuMVC.StructureMap;
using FubuValidation;
using FubuValidation.StructureMap;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
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
                         s.ConnectImplementationsToTypesClosing(typeof (ModelRule<>));
                     });

            For<ISettingsProvider>().Use<AppSettingsProvider>();
            SetAllProperties(s => s.Matching(p => p.Name.EndsWith("Settings")));

            ForSingletonOf<IDocumentStore>()
                .Use(ctx =>
                         {
                             var store = new DocumentStore {ConnectionStringName = "RavenDb"};
                             store.Conventions.IdentityPartsSeparator = "-";
                             store.Initialize();
                             IndexCreation.CreateIndexes(typeof (ConfigureFubuMvc).Assembly, store);
                             return store;
                         });

            For<IDocumentSession>()
                .Use(ctx => ctx.GetInstance<IDocumentStore>().OpenSession());

            For<IPrincipalFactory>()
                .Use<FubuPrincipalFactory>();

            this.FubuValidation();

            For<IValidationSource>().Add<RuleSource>();
        }
    }
}