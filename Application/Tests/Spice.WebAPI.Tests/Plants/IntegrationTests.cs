using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Spice.WebAPI.Tests.Plants
{
    [TestFixture]
    public class IntegrationTests
    {
        [TestCase("/api/plants", TestName = "GET list of plants returns correct content type")]
        [TestCase("/api/plants/5", TestName = "GET single plant return correct content type")]
        public async Task GetListReturnsSuccessAndCorrectContentType(string url)
        {
            throw new NotImplementedException();
        }

        [TestCase(TestName = "POST plant returns \"Created\", Location header set and correct content type")]
        public async Task PostNewPlantReturnsSuccessAndLocationHeaderWithIdAndCorrectContentType()
        {
            throw new NotImplementedException();
        }

        [TestCase(TestName = "PUT plant returns updated plant and correct content type")]
        public async Task PutPlantReturnsSuccessAndUpdatedPlantAndCorrectContentType()
        {
            throw new NotImplementedException();
        }

        [TestCase(TestName = "DELETE plant returns \"No Content\" and correct content type")]
        public async Task DeletePlantReturnsNoContentAndCorrectContentType()
        {
            throw new NotImplementedException();
        }
    }
}