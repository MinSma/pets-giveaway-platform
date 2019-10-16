using PGP.Application.Categories.Commands.CreateCategory;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Categories
{
    public class UpdateCategoryTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UpdateCategoryTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnsUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validId = 1;

            var command = new CreateCategoryCommand
            {
                Title = "Snakes"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/categories/{validId}", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenNotAdminUser_ReturnsForbiddenStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var validId = 1;

            var command = new CreateCategoryCommand
            {
                Title = "Snakes"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/categories/{validId}", content);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GivenId_ReturnsSuccessStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var validId = 1;

            var command = new CreateCategoryCommand
            {
                Title = "Snakes"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/categories/{validId}", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenIdAndExistingTitle_ReturnsConflictStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var validId = 1;

            var command = new CreateCategoryCommand
            {
                Title = "Cats"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/categories/{validId}", content);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var invalidId = 10;

            var command = new CreateCategoryCommand
            {
                Title = "Snakes"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/categories/{invalidId}", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
