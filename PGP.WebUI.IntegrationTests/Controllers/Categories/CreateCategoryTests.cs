using PGP.Application.Categories.Commands.CreateCategory;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Categories
{
    public class CreateCategoryTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CreateCategoryTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnsUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new CreateCategoryCommand
            {
                Title = "Snakes"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/categories", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenAuthorizedAdminAndExistingCategoryTitle_ReturnsConflictStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new CreateCategoryCommand
            {
                Title = "Dogs"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/categories", content);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }
    }
}
