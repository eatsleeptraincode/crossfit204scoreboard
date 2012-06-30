using System;
using System.Collections.Generic;
using System.Linq;
using CrossFit204ScoreBoard.Web.Models;
using FubuMVC.Core.UI.Configuration;
using HtmlTags;

namespace CrossFit204ScoreBoard.Web.Config.Html
{
    public class HtmlBuilders
    {
        public static HtmlTag GenderBuilder(ElementRequest request)
        {
            var genders = Enum.GetValues(typeof (Gender)).Cast<Gender>().ToList();
            var tag = new SelectTag(t =>
                                        {
                                            genders.Each(g => t.Option(g.ToString(), g));
                                            t.SelectByValue((request.RawValue ?? Gender.Male).ToString());
                                        });
            return tag;
        }

        public static HtmlTag CheckBoxBuilder(ElementRequest request)
        {
            var isChecked = request.Value<bool>();
            var tag = new CheckboxTag(isChecked);
            tag.Attr("name", request.Accessor.Name);
            tag.AddClass("checkbox");
            return tag;
        }
    }
}