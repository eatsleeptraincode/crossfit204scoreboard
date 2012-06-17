using System;
using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using FubuCore.Reflection;
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
            Editors.IfPropertyIs<Gender>().BuildBy(HtmlBuilders.GenderBuilder);
            Editors.IfPropertyIs<bool>().BuildBy(HtmlBuilders.CheckBoxBuilder);
            Editors.IfPropertyIs<Time>().BuildBy(HtmlBuilders.TimeBuilder);
        }
    }

    public class HtmlBuilders
    {
        public static HtmlTag GenderBuilder(ElementRequest request)
        {
            var genders = Enum.GetValues(typeof (Gender)).Cast<Gender>().ToList();
            var tag = new SelectTag(t =>
                                        {
                                            genders.Each(g => t.Option(g.ToString(), g));
                                            t.DefaultOption(request.RawValue.ToString());
                                        });
            return tag;
        }

        public static HtmlTag CheckBoxBuilder(ElementRequest request)
        {
            var isChecked = request.Value<bool>();
            var tag = new CheckboxTag(isChecked);
            tag.Attr("name", request.Accessor.Name);
            return tag;
        }

        public static HtmlTag TimeBuilder(ElementRequest request)
        {
            var time = request.Value<Time>();
            var minutes = ReflectionHelper.GetProperty<Time>(t => t.Minutes).Name;
            var seconds = ReflectionHelper.GetProperty<Time>(t => t.Seconds).Name;
            var baseName = request.Accessor.Name;

            var tag = new TextboxTag(baseName + minutes, time.Minutes.ToString())
                .Append(new LiteralTag(":"))
                .Append(new TextboxTag(baseName + seconds, time.Seconds.ToString()));

            return tag;
        }
    }
}