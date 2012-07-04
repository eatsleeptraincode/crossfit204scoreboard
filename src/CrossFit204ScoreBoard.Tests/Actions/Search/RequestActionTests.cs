using CrossFit204ScoreBoard.Web.Actions.Search;
using FubuTestingSupport;
using NUnit.Framework;

namespace CrossFit204ScoreBoard.Tests.Actions.Search
{
    [TestFixture]
    public class RequestActionTests : InteractionContext<RequestAction>
    {
        [Test]
        public void ShouldReturnDataForView()
        {
            var result = ClassUnderTest.Get(new SearchRequestRequest());
            result.ShouldBeOfType<SearchRequestViewModel>();
        }
    }
}