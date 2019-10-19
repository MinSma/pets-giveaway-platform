using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Likes
{
    public class DeleteLikeTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeleteLikeTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUserIsUnauthorized_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validUserId = 1;
            var validPetId = 1;

            var response = await client.DeleteAsync($"/api/users/{validUserId}/pets/{validPetId}/likes");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserWhichIsNotAuthorizedUserIdAndPetId_ReturnUnauthrorizedStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var validUserId = 2;
            var validPetId = 1;

            var response = await client.DeleteAsync($"/api/users/{validUserId}/pets/{validPetId}/likes");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidUserId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var invalidUserId = 10;
            var validPetId = 1;

            var response = await client.DeleteAsync($"/api/users/{invalidUserId}/pets/{validPetId}/likes");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidPetId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var validUserId = 1;
            var invalidPetId = 10;

            var response = await client.DeleteAsync($"/api/users/{validUserId}/pets/{invalidPetId}/likes");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidPetIdIsNotLiked_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var validUserId = 3;
            var validPetId = 2;

            var response = await client.DeleteAsync($"/api/users/{validUserId}/pets/{validPetId}/likes");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidPetIdAndValidUserId_ReturnOkStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var validUserId = 3;
            var validPetId = 1;

            var response = await client.DeleteAsync($"/api/users/{validUserId}/pets/{validPetId}/likes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
