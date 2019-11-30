using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Categories
{
    public class DeleteCategoryTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeleteCategoryTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnsUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validId = 1;

            var response = await client.DeleteAsync($"/api/categories/{validId}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenNotAdminUser_ReturnsForbiddenStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var validId = 1;

            var response = await client.DeleteAsync($"/api/categories/{validId}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidId_ReturnsSuccessStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var validId = 1;

            var response = await client.DeleteAsync($"/api/categories/{validId}");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var invalidId = 10;

            var response = await client.DeleteAsync($"/api/categories/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
