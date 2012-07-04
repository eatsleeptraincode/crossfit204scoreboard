using CrossFit204ScoreBoard.Web.Actions.Accounts;
using CrossFit204ScoreBoard.Web.Config;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Validation;
using FubuTestingSupport;
using FubuValidation;
using NUnit.Framework;
using StructureMap;
using FubuCore.Reflection;

namespace CrossFit204ScoreBoard.Tests.Config
{
    [TestFixture]
    public class ViewModelDescriptorTests
    {
        [Test]
        public void ShouldGetRequestForViewModel()
        {
            var d = new ViewModelDescriptor(ObjectFactory.GetInstance<BehaviorGraph>());
            var call = new ActionCall(typeof (RegisterAction),ReflectionHelper.GetMethod<RegisterAction>(a => a.Post(new RegisterViewModel())));
            var context = new ValidationFailure(call, new Notification(typeof(RegisterViewModel)), new RegisterViewModel());
            var request = d.DescribeModelFor(context);
            request.ShouldEqual(typeof (RegisterRequest));
        }

        [Test]
        public void ShouldReturnNullForUnregisteredAction()
        {
            var d = new ViewModelDescriptor(ObjectFactory.GetInstance<BehaviorGraph>());
            var call = new ActionCall(typeof(UnRegisteredAction), ReflectionHelper.GetMethod<UnRegisteredAction>(a => a.Get(new object())));
            var context = new ValidationFailure(call, new Notification(typeof(object)), new object());
            var request = d.DescribeModelFor(context);
            request.ShouldBeNull();
        }
         
    }

    public class UnRegisteredAction
    {
        public object Get(object input)
        {
            return input;
        }
    }
}