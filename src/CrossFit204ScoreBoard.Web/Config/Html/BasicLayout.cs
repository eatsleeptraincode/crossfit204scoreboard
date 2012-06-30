using System.Collections.Generic;
using FubuCore;
using FubuMVC.Core.UI.Forms;
using HtmlTags;

namespace CrossFit204ScoreBoard.Web.Config.Html
{
    public class BasicLayout : ILabelAndFieldLayout
    {
        public IEnumerable<HtmlTag> AllTags()
        {
            yield return LabelTag;
            yield return BodyTag;
        }

        public HtmlTag LabelTag { get; set; }
        public HtmlTag BodyTag { get; set; }

        public override string ToString()
        {
            return "{0}{1}".ToFormat(LabelTag, BodyTag);
        }
    }
}