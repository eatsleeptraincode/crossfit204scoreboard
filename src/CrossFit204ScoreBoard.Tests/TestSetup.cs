using CrossFit204ScoreBoard.Web.Config;
using FubuMVC.Core;
using FubuMVC.StructureMap;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests
{
    [SetUpFixture]
    public class TestSetup
    {
        [SetUp]
        public void Init()
        {
            FubuApplication
                .For<ConfigureFubuMvc>()
                .StructureMapObjectFactory()
                .Bootstrap();
        }
    }
}