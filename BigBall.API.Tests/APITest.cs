using AutoMapper;
using BigBall.API.Mappers;
using Xunit;

namespace BigBall.API.Tests
{
    public class APITest
    {

        [Fact]
        public void TestMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new APIMapper());
            });

            config.AssertConfigurationIsValid();
        }
    }
}