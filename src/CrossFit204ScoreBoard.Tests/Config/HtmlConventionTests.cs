using CrossFit204ScoreBoard.Web.Config;
using CrossFit204ScoreBoard.Web.Models;
using FubuCore;
using FubuCore.Reflection;
using FubuMVC.Core.UI.Configuration;
using NUnit.Framework;
using Rhino.Mocks;
using FubuTestingSupport;

namespace CrossFit204ScoreBoard.Tests.Config
{
    [TestFixture]
    public class GenderBuilderTests
    {
        [Test]
        public void SelectWithMaleAndFemaleOptions()
        {
            var accessor = ReflectionHelper.GetAccessor<Athlete>(a => a.Gender);
            var locator = MockRepository.GenerateStub<IServiceLocator>();
            var request = new ElementRequest(new Athlete{Gender = Gender.Female}, accessor, locator);
            var tag = HtmlBuilders.GenderBuilder(request);
            tag.ToString().ShouldEqual("<select><option value=\"Male\">Male</option><option value=\"Female\" selected=\"selected\">Female</option></select>");
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
            tag.ToString().ShouldEqual("<input type=\"checkbox\" checked=\"true\" name=\"" + accessor.Name +"\" />");
        }
        
        [Test]
        public void NonCheckedWhenFalse()
        {
            var request = new ElementRequest(new Workout { TrackTime = false }, accessor, locator);
            var tag = HtmlBuilders.CheckBoxBuilder(request);
            tag.ToString().ShouldEqual("<input type=\"checkbox\" name=\"" + accessor.Name + "\" />");
        }
    }

}