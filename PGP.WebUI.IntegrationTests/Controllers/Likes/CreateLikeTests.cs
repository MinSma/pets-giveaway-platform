using PGP.Application.Likes.Commands.CreateLike;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Likes
{
    public class CreateLikeTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CreateLikeTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUserIsUnauthorized_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validUserId = 1;
            var validPetId = 1;

            var content = Utilities.GetRequestContent(null);

            var response = await client.PostAsync($"/api/users/{validUserId}/pets/{validPetId}/likes", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserIsAuthorizedValidUserIdAndPetId_ReturnOkStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var validUserId = 1;
            var validPetId = 1;

            var response = await client.PostAsync($"/api/users/{validUserId}/pets/{validPetId}/likes", null);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserIsAuthorizedValidUserIdAndPetIdSecondTimeInARow_ReturnConflictStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var validUserId = 1;
            var validPetId = 1;

            var response = await client.PostAsync($"/api/users/{validUserId}/pets/{validPetId}/likes", null);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }
    }
}
