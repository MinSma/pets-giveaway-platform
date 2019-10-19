using PGP.Application.Users.UserLogin;
using PGP.WebUI.IntegrationTests.Common;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Users
{
    public class UserLoginTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UserLoginTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUser_ReturnsJwtToken()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new UserLoginCommand
            {
                Email = "petras@petrauskas.com",
                Password = "password"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/users/login", content);

            var responseContent = await Utilities.GetResponseContent<UserLoginCommandResponse>(response);

            Assert.IsType<UserLoginCommandResponse>(responseContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GivenModerator_ReturnsJwtToken()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new UserLoginCommand
            {
                Email = "moderator@email.com",
                Password = "password"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/users/login", content);

            var responseContent = await Utilities.GetResponseContent<UserLoginCommandResponse>(response);

            Assert.IsType<UserLoginCommandResponse>(responseContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GivenAdmin_ReturnsJwtToken()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new UserLoginCommand
            {
                Email = "admin@admin.com",
                Password = "password"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/users/login", content);

            var responseContent = await Utilities.GetResponseContent<UserLoginCommandResponse>(response);

            Assert.IsType<UserLoginCommandResponse>(responseContent);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GivenWrongRole_Return()
        {
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _factory.GetAuthenticatedClientAsync("Random"));

            Assert.Equal("Specified Role not exists", ex.Message);
        }

        [Fact]
        public async Task GivenWrongLoginData_ReturnsUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new UserLoginCommand
            {
                Email = "sdadsaas@admin.com",
                Password = "password"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/users/login", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
