using CrossFit204ScoreBoard.Web.Config.Html;
using CrossFit204ScoreBoard.Web.Models;
using FubuCore;
using FubuCore.Reflection;
using FubuMVC.Core.UI.Configuration;
using FubuTestingSupport;
using HtmlTags;
using NUnit.Framework;
using Rhino.Mocks;

namespace CrossFit204ScoreBoard.Tests.Config.Html
{
    [TestFixture]
    public class GenderBuilderTests
    {
        HtmlTag tag;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var accessor = ReflectionHelper.GetAccessor<Athlete>(a => a.Gender);
            var locator = MockRepository.GenerateStub<IServiceLocator>();
            var request = new ElementRequest(new Athlete { Gender = Gender.Female }, accessor, locator);
            tag = HtmlBuilders.GenderBuilder(request);
        }

        [Test]
        public void ShouldBeSelectTag()
        {
            tag.ShouldBeOfType<SelectTag>();
        }
 
        [Test]
        public void FirstOption()
        {
            var firstOption = tag.Children[0];
            firstOption.TagName().ShouldEqual("option");
            firstOption.Attr("value").ShouldEqual("Male");
            firstOption.Text().ShouldEqual("Male");
        }

        [Test]
        public void SecondOption()
        {
            var secondOption = tag.Children[1];
            secondOption.TagName().ShouldEqual("option");
            secondOption.Attr("value").ShouldEqual("Female");
            secondOption.Text().ShouldEqual("Female");
            secondOption.Attr("selected").ShouldEqual("selected");
        }
    }

    [TestFixture]
    public class CheckBoxBuilderTests
    {
        private Accessor accessor = ReflectionHelper.GetAccessor<Workout>(a => a.TrackTime);
        private IServiceLocator locator = MockRepository.GenerateStub<IServiceLocator>();

        [Test]
        public void CheckedWhenTrue()
        {
            var request = new ElementRequest(new Workout { TrackTime = true }, accessor, locator);
            var tag = HtmlBuilders.CheckBoxBuilder(request);
            tag.ShouldBeOfType<CheckboxTag>();
            tag.Attr("checked").ShouldEqual("true");
            tag.GetClasses().ShouldContain(c => c == "checkbox");
            tag.Attr("name").ShouldEqual(accessor.Name);
        }
        
        [Test]
        public void NonCheckedWhenFalse()
        {
            var request = new ElementRequest(new Workout { TrackTime = false }, accessor, locator);
            var tag = HtmlBuilders.CheckBoxBuilder(request);
            tag.ShouldBeOfType<CheckboxTag>();
            tag.HasAttr("checked").ShouldBeFalse();
            tag.GetClasses().ShouldContain(c => c == "checkbox");
            tag.Attr("name").ShouldEqual(accessor.Name);
        }
    }

}