using PGP.Application.Pets.Commands.UpdatePet;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Pets
{
    public class UpdatePetTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UpdatePetTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new UpdatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = 0.2,
                Height = 0.3,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 1,
                UserId = 3
            };

            var content = ClientUtilities.GetRequestContent(command);

            var validPetId = 1;

            var response = await client.PutAsync($"/api/pets/{validPetId}", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenAuthorizedUserHasUserRole_ReturnForbiddenStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var command = new UpdatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = 0.2,
                Height = 0.3,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 1,
                UserId = 3
            };

            var content = ClientUtilities.GetRequestContent(command);

            var validPetId = 1;

            var response = await client.PutAsync($"/api/pets/{validPetId}", content);

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserIdIsNotAuthorizedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var command = new UpdatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = 0.2,
                Height = 0.3,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 1,
                UserId = 4
            };

            var content = ClientUtilities.GetRequestContent(command);

            var validPetId = 1;

            var response = await client.PutAsync($"/api/pets/{validPetId}", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidPetId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var command = new UpdatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = 0.2,
                Height = 0.3,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 10,
                UserId = 3
            };

            var content = ClientUtilities.GetRequestContent(command);

            var invalidPetId = 10;

            var response = await client.PutAsync($"/api/pets/{invalidPetId}", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidCategoryId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var command = new UpdatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = 0.2,
                Height = 0.3,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 10,
                UserId = 2
            };

            var content = ClientUtilities.GetRequestContent(command);

            var validPetId = 1;

            var response = await client.PutAsync($"/api/pets/{validPetId}", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidData_ReturnOkStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var command = new UpdatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = 0.2,
                Height = 0.3,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 1,
                UserId = 2
            };

            var content = ClientUtilities.GetRequestContent(command);

            var validPetId = 1;

            var response = await client.PutAsync($"/api/pets/{validPetId}", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
