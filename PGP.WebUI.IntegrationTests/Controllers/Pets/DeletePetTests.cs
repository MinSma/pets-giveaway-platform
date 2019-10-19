using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Pets
{
    public class DeletePetTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeletePetTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validPetId = 1;

            var response = await client.DeleteAsync($"/api/pets/{validPetId}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenAuthorizedUserHasUserRole_ReturnForbiddenStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var validPetId = 1;

            var response = await client.DeleteAsync($"/api/pets/{validPetId}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidPetId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var invalidPetId = 10;

            var response = await client.DeleteAsync($"/api/pets/{invalidPetId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidData_ReturnOkStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var validPetId = 1;

            var response = await client.DeleteAsync($"/api/pets/{validPetId}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
