using AutoMapper;
using NUnit.Framework;
using Spice.AutoMapper;

namespace Spice.Application.Tests.Common
{
    [TestFixture]
    internal sealed class AutoMapperTests
    {
        [TestCase(TestName = "AutoMapper configuration is valid")]
        public void ConfigurationIsValid()
        {
            // Given
            IMapper mapper = AutoMapperFactory.CreateMapper();

            // When
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            // Then
            Assert.Pass();
        }
    }
}