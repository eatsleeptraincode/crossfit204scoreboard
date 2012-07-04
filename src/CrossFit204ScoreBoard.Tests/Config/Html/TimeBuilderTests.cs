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
    public class TimeBuilderEditorTests
    {
        private HtmlTag tag;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var accessor = ReflectionHelper.GetAccessor<Score>(a => a.Time);
            var locator = MockRepository.GenerateStub<IServiceLocator>();
            var request = new ElementRequest(new Score(), accessor, locator);
            tag = TimeBuilder.BuildEditor(request);
        }

        [Test]
        public void FirstInput()
        {
            var firstTag  = tag.Children[0];
            firstTag.ShouldBeOfType<TextboxTag>();
            firstTag.GetClasses().ShouldContain(a => a == "short");
            firstTag.Attr("name").ShouldEqual("TimeMinutes");
        }

        [Test]
        public void Separator()
        {
            tag.Children[1].ToString().ShouldEqual(" : ");
        }

        [Test]
        public void SecondInput()
        {
            var secondTag = tag.Children[2];
            secondTag.ShouldBeOfType<TextboxTag>();
            secondTag.GetClasses().ShouldContain(a => a == "short");
            secondTag.Attr("name").ShouldEqual("TimeSeconds");
        }
    }

    [TestFixture]
    public class TimeBuilderDisplayTests
    {

        private HtmlTag tag;
        private int minutes;
        private decimal seconds;

        [TestFixtureSetUp]
        public void SetUp()
        {
            var accessor = ReflectionHelper.GetAccessor<Score>(a => a.Time);
            var locator = MockRepository.GenerateStub<IServiceLocator>();
            minutes = 12;
            seconds = 4.5M;
            var request = new ElementRequest(new Score{Time = new Time{Minutes = minutes, Seconds = seconds}}, accessor, locator);
            tag = TimeBuilder.BuildDisplay(request);
        }

        [Test]
        public void MinuteDisplay()
        {
            var minuteTag = tag.Children[0];
            minuteTag.Attr("name").ShouldEqual("TimeMinutes");
            minuteTag.Text().ShouldEqual(minutes.ToString("##"));
        }

        [Test]
        public void Separator()
        {
            tag.Children[1].ToString().ShouldEqual(":");
        }

        [Test]
        public void SecondDisplay()
        {
            var secondTag = tag.Children[2];
            secondTag.Attr("name").ShouldEqual("TimeSeconds");
            secondTag.Text().ShouldEqual(seconds.ToString("0#.#"));
        }
        
    }
}