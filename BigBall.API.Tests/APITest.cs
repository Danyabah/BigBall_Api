using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

namespace BigBall.API.Tests
{
    public class APITest
    {

        [Fact]
        public void TestValidate()
        {
            var item = 1.March(2022).At(20, 30).AsLocal();
            var item2 = 2.March(2022).At(20, 30).AsLocal();
            item.Should().NotBe(item2);
        }
    }
}
