using FubuMVC.Core.UI;
using FubuMVC.Core.UI.Configuration;
using HtmlTags;

namespace CrossFit204ScoreBoard.Web.Config
{
    public class ConfigureHtmlConventions  : HtmlConventionRegistry
    {
        public ConfigureHtmlConventions()
        {
            Editors.If(a => a.Accessor.FieldName.Contains("Password")).Attr("type","password");
            Editors.If(a => a.Accessor.Name.EndsWith("Id")).Attr("type","hidden");
            Editors.IfPropertyIs<bool>().BuildBy(CheckBoxBuilder);
        }

        private HtmlTag CheckBoxBuilder(ElementRequest request)
        {
            var isChecked = request.Value<bool>();
            var tag = new CheckboxTag(isChecked);
            tag.Attr("name", request.Accessor.Name);
            return tag;
        }
    }
}