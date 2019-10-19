using PGP.Application.Pets.Commands.CreatePet;
using PGP.WebUI.IntegrationTests.Common;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Pets
{
    public class CreatePetTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CreatePetTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new CreatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = null,
                Height = null,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 1,
                UserId = 1
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/api/pets", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenUnauthorizedUserId_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new CreatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = null,
                Height = null,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 1,
                UserId = 10
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/api/pets", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidCategoryId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new CreatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = null,
                Height = null,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 10,
                UserId = 1
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/api/pets", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidData_ReturnOkStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var command = new CreatePetCommand
            {
                Name = "Wau",
                Age = 5,
                Gender = Domain.Enums.Gender.Male,
                Weight = null,
                Height = null,
                IsSterilized = true,
                Description = "Something about",
                PhotoCode = "",
                CategoryId = 1,
                UserId = 1
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/api/pets", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
