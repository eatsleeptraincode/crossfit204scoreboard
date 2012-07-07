using CrossFit204ScoreBoard.Web.Actions;
using CrossFit204ScoreBoard.Web.Behaviours;
using CrossFit204ScoreBoard.Web.Config;
using CrossFit204ScoreBoard.Web.Security;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Security.AntiForgery;
using FubuMVC.StructureMap;
using FubuMVC.Validation;
using FubuValidation;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests
{
    [SetUpFixture]
    public class TestSetup
    {
        [SetUp]
        public void Init()
        {
            FubuApplication
                .For<TestRegistry>()
                .StructureMapObjectFactory();
        }
    }

    public class TestRegistry : FubuRegistry
    {
        public TestRegistry()
        {
            Applies.ToAssemblyContainingType<ConfigureFubuMvc>();

            Actions.IncludeTypesNamed(n => n.EndsWith("Action"));

            Routes
                .HomeIs<ScoreBoardRequest>()
                .IgnoreNamespaceForUrlFrom<UrlRoot>()
                .IgnoreClassSuffix("Action")
                .IgnoreMethodsNamed("Get")
                .IgnoreMethodsNamed("Post")
                .ConstrainToHttpMethod(c => c.Method.Name.Equals("Get"), "GET")
                .ConstrainToHttpMethod(c => c.Method.Name.Equals("Post"), "POST");

            Policies
                .Add<AttachAuthenticationPolicy>()
                .Add<AttachUserPolicy>()
                .Add<AttachAdminPolicy>();

            this.Validation(v =>
            {
                v.Actions
                    .Include(a => a.HasInput && a.InputType().Name.EndsWith("ViewModel"));
                v.Failures
                    .If(a => a.InputType() != null && a.InputType().Name.EndsWith("ViewModel"))
                    .TransferBy<ViewModelDescriptor>();
            });
        }
    }
}