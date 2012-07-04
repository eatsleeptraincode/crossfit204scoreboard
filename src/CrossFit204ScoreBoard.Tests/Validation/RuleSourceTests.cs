using System.Linq;
using CrossFit204ScoreBoard.Web.Validation;
using FubuTestingSupport;
using NUnit.Framework;
using StructureMap;

namespace CrossFit204ScoreBoard.Tests.Validation
{
    [TestFixture]
    public class RuleSourceTests
    {
        [Test]
        public void ShouldReturnAllRulesForType()
        {
            var goodRule1 = new GoodRule1();
            var goodRule2 = new GoodRule2();
            var badRule1 = new BadRule1();
            var container = new Container(c =>
                                              {
                                                  c.For(typeof (ModelRule<GoodModel>)).Add(goodRule1);
                                                  c.For(typeof (ModelRule<GoodModel>)).Add(goodRule2);
                                                  c.For(typeof (ModelRule<BadModel>)).Add(badRule1);
                                              });
            var source = new RuleSource(container);
            var rules = source.RulesFor(typeof (GoodModel)).ToList();
            rules.ShouldContain(goodRule1);
            rules.ShouldContain(goodRule2);
            rules.ShouldNotContain(badRule1);
        }
    }

    public class GoodRule1 : ModelRule<GoodModel>
    {
        protected override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
    public class GoodRule2: ModelRule<GoodModel>
    {
        protected override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
    public class BadRule1 : ModelRule<BadModel>
    {
        protected override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
    public class GoodModel
    {
    }
    public class BadModel
    {
    }
}