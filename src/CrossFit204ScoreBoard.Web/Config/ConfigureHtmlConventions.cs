using CrossFit204ScoreBoard.Web.Config.Html;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.UI;
using FubuValidation;

namespace CrossFit204ScoreBoard.Web.Config
{
    public class ConfigureHtmlConventions  : HtmlConventionRegistry
    {
        public ConfigureHtmlConventions()
        {
            Editors.If(a => a.Accessor.FieldName.Contains("Password")).Attr("type","password");
            Editors.If(a => a.Accessor.Name.EndsWith("Id")).Attr("type","hidden");
            Editors.IfPropertyIs<Gender>().BuildBy(HtmlBuilders.GenderBuilder);
            Editors.IfPropertyIs<bool>().BuildBy(HtmlBuilders.CheckBoxBuilder);
            Editors.IfPropertyIs<int>().AddClass("digits");
            Editors.IfPropertyIs<decimal>().AddClass("number");
            Editors.ModifyForAttribute<RequiredAttribute>(t => t.AddClass("required"));
            Editors.IfPropertyIs<Time>().BuildBy(TimeBuilder.BuildEditor);
            Displays.IfPropertyIs<Time>().BuildBy(TimeBuilder.BuildDisplay);
            UseLabelAndFieldLayout<BasicLayout>();
        }
    }
}