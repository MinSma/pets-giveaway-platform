using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Comments
{
    public class DeleteCommentTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeleteCommentTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validId = 1;

            var response = await client.DeleteAsync($"/api/comments/{validId}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidCommentId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var invalidId = 10;

            var response = await client.DeleteAsync($"/api/comments/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenAuthenticatedUserIsNotCommentAuthorOrAdmin_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var validId = 1;

            var response = await client.DeleteAsync($"/api/comments/{validId}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidData_ReturnOkStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var validId = 2;

            var response = await client.DeleteAsync($"/api/comments/{validId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
