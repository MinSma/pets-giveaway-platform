using PGP.Application.Categories.Queries.GetCategoryById;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Categories
{
    public class GetCategoryByIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetCategoryByIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenId_ReturnCategoryViewModel()
        {
            var client = await _factory.GetAnonymousClient();
            var id = 1;
            var response = await client.GetAsync($"/api/categories/{id}");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<GetCategoryByIdQueryResponse>(response);

            Assert.IsType<GetCategoryByIdQueryResponse>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
        {
            var client = await _factory.GetAnonymousClient();
            var invalidId = 10;
            var response = await client.GetAsync($"/api/categories/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
