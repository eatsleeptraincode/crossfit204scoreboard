using System.Linq;
using System.Threading;
using CrossFit204ScoreBoard.Web.Config;
using CrossFit204ScoreBoard.Web.Validation;
using FubuTestingSupport;
using FubuValidation;
using NUnit.Framework;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;

namespace CrossFit204ScoreBoard.Tests
{
    public abstract class RavenContextWithIndex<T> : InteractionContext<T> where T: class
    {
        protected EmbeddableDocumentStore Store;
        protected IDocumentSession Session;

        [TestFixtureSetUp]
        public void Init()
        {
            Store = new EmbeddableDocumentStore { RunInMemory = true };
            Store.Configuration.RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true;
            Store.Initialize();
            IndexCreation.CreateIndexes(typeof(ConfigureFubuMvc).Assembly, Store);
            Session = Store.OpenSession();
            BeforeIndexing();
            Session.SaveChanges();
            WaitForIndexes();
        }

        protected abstract void BeforeIndexing();

        [SetUp]
        public void ConfigureServices()
        {
            Services.Inject(Session);
            Context();
        }

        protected virtual void Context()
        {
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            Session.Dispose();
            Store.Dispose();
        }

        protected void WaitForIndexes()
        {
            while (Session.Advanced.DatabaseCommands.GetStatistics().StaleIndexes.Length != 0)
            {
                Thread.Sleep(10);
            }
        }
        
    }

    public class ValidationRuleContext<T, TR> : InteractionContext<T> where T : ModelRule<TR> where TR : class
    {
        protected EmbeddableDocumentStore Store;
        protected IDocumentSession Session;
        protected Notification Notification;
        private IValidator provider;

        [TestFixtureSetUp]
        public void Init()
        {
            Store = new EmbeddableDocumentStore { RunInMemory = true };
            Store.Configuration.RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true;
            Store.Initialize();
            Session = Store.OpenSession();
        }

        [SetUp]
        public void ConfigureServices()
        {
            Services.Inject(Session);
            
        }

        public void Validate(TR model)
        {
            provider = MockFor<IValidator>();
            Notification = new Notification(typeof(TR));
            var context = new ValidationContext(provider, Notification, model);
            ClassUnderTest.Validate(context);
        }


        [TestFixtureTearDown]
        public void Dispose()
        {
            Session.Dispose();
            Store.Dispose();
        }
    }

    public static class NotificationExtensions
    {
        public static string FirstErrorKey(this Notification notification)
        {
            return notification.AllMessages.First().StringToken.Key;
        }
    }

    public class RavenContext<T> : InteractionContext<T> where T : class
    {
        protected EmbeddableDocumentStore Store;
        protected IDocumentSession Session;

        [TestFixtureSetUp]
        public void Init()
        {
            Store = new EmbeddableDocumentStore {RunInMemory = true};
            Store.Configuration.RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true;
            Store.Initialize();
            Session = Store.OpenSession();
            OneTimeSetup();
        }

        [SetUp]
        public void ConfigureServices()
        {
            Services.Inject(Session);
            SetupForEach();
        }

        protected virtual void SetupForEach()
        {
        }

        protected virtual void OneTimeSetup()
        {
        }


        [TestFixtureTearDown]
        public void Dispose()
        {
            Session.Dispose();
            Store.Dispose();
        }
    }
}