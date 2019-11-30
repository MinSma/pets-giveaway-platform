using PGP.Application.Users.Commands.UpdateUser;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Users
{
    public class UpdateUserTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UpdateUserTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnsUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validId = 1;

            var command = new UpdateUserCommand
            {
                PhoneNumber = "865344095",
                FirstName = "Vard",
                LastName = "Pavard"
            };

            var content = ClientUtilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/users/{validId}", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenWrongUserForEdit_ReturnsUnauthrorizedStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var validId = 1;

            var command = new UpdateUserCommand
            {
                PhoneNumber = "865344095",
                FirstName = "Vard",
                LastName = "Pavard"
            };

            var content = ClientUtilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/users/{validId}", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenId_ReturnsSuccessStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var validId = 1;

            var command = new UpdateUserCommand
            {
                PhoneNumber = "865344095",
                FirstName = "Vard",
                LastName = "Pavard"
            };

            var content = ClientUtilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/users/{validId}", content);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var invalidId = 10;

            var command = new UpdateUserCommand
            {
                PhoneNumber = "865344095",
                FirstName = "Vard",
                LastName = "Pavard"
            };

            var content = ClientUtilities.GetRequestContent(command);

            var response = await client.PutAsync($"/api/users/{invalidId}", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
