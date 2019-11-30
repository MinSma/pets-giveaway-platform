using PGP.Application.Users.Commands.CreateUser;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Users
{
    public class CreateUserTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CreateUserTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUserEmailAndExists_ReturnsConflictStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new CreateUserCommand
            {
                Email = "petras@petrauskas.com",
                Password = "password",
                PhoneNumber = "865344095",
                FirstName = "Vard",
                LastName = "Pavard"
            };

            var content = ClientUtilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/users/register", content);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        [Fact]
        public async Task GivenCorrectUserData_ReturnsOk()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new CreateUserCommand
            {
                Email = "jonas@petrauskas.com",
                Password = "password",
                PhoneNumber = "865344095",
                FirstName = "Vard",
                LastName = "Pavard"
            };

            var content = ClientUtilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/users/register", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
