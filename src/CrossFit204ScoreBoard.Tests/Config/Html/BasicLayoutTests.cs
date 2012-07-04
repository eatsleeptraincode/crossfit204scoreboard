using System.Linq;
using CrossFit204ScoreBoard.Web.Config.Html;
using FubuCore;
using FubuTestingSupport;
using HtmlTags;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Config.Html
{
    [TestFixture]
    public class BasicLayoutTests
    {
        private readonly BasicLayout layout = new BasicLayout {LabelTag = labelTag, BodyTag = bodyTag};
        private static HtmlTag labelTag = new HtmlTag("label");
        private static HtmlTag bodyTag = new HtmlTag("body");

        [Test]
        public void AllTagsReturnsLabelAndBody()
        {
            var allTags = layout.AllTags().ToArray();
            allTags[0].ShouldEqual(labelTag);
            allTags[1].ShouldEqual(bodyTag);
        }

        [Test]
        public void ToStringReturnsBothTagsAsStrings()
        {
            layout.ToString().ShouldEqual("{0}{1}".ToFormat(labelTag, bodyTag));
        }
    }
}