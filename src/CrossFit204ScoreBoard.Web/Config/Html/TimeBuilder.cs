using CrossFit204ScoreBoard.Web.Models;
using FubuCore.Reflection;
using FubuMVC.Core.UI.Configuration;
using HtmlTags;
using HtmlTags.Extended.Attributes;

namespace CrossFit204ScoreBoard.Web.Config.Html
{
    public static class TimeBuilder
    {
        private static readonly string Minutes = ReflectionHelper.GetProperty<Time>(t => t.Minutes).Name;
        private static readonly string Seconds = ReflectionHelper.GetProperty<Time>(t => t.Seconds).Name;

        public static HtmlTag BuildEditor(ElementRequest request)
        {
            var time = request.Value<Time>();
            var baseName = request.Accessor.Name;
            var tag = new HtmlTag("span", c =>
                                              {
                                                  c.Append(BuildTimeElementEditorTag(baseName + Minutes, time.Minutes, "##"));
                                                  c.Append(new LiteralTag(" : "));
                                                  c.Append(BuildTimeElementEditorTag(baseName + Seconds, time.Seconds, "##.#"));
                                              });

            return tag;
        }

        private static HtmlTag BuildTimeElementEditorTag(string propName, decimal value, string format)
        {
            return new TextboxTag(propName, value.ToString(format)).AddClass("short");
        }

        public static HtmlTag BuildDisplay(ElementRequest request)
        {
            var time = request.Value<Time>();
            var baseName = request.Accessor.Name;
            var tag = new HtmlTag("span", c =>
                                              {
                                                  c.Append(BuildTimeElementDisplayTag(baseName + Minutes, time.Minutes, "##"));
                                                  c.Append(new LiteralTag(":"));
                                                  c.Append(BuildTimeElementDisplayTag(baseName + Seconds, time.Seconds, "0#.#"));
                                              });
            return tag;
        }

        private static HtmlTag BuildTimeElementDisplayTag(string propName, decimal value, string format)
        {
            return new HtmlTag("span", c =>
                                           {
                                               c.Name(propName);
                                               c.Text(value.ToString(format));
                                           });
        }
    }
}