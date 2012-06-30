using System;
using System.Linq.Expressions;
using CrossFit204ScoreBoard.Web.Models;
using FubuCore.Reflection;
using FubuMVC.Core.UI.Configuration;
using HtmlTags;
using HtmlTags.Extended.Attributes;

namespace CrossFit204ScoreBoard.Web.Config.Html
{
    public static class TimeBuilder
    {
        public static HtmlTag BuildTime(ElementRequest request)
        {
            var time = request.Value<Time>();
            var tm = BuildTimeElementTag(t => t.Minutes, time.Minutes, "##");
            var ts = BuildTimeElementTag(t => t.Seconds, time.Seconds, "0#.#");

            var tag = tm
                .Append(new LiteralTag(":"))
                .Append(ts);

            return tag;
        }

        private static HtmlTag BuildTimeElementTag(Expression<Func<Time, object>> prop, decimal value, string format)
        {
            var seconds = ReflectionHelper.GetProperty(prop).Name;
            return new HtmlTag("span", c =>
                                           {
                                               c.Name("Time" + seconds);
                                               c.Text(value.ToString(format));
                                               c.AddClass("short");
                                           });
        }
    }
}