using PGP.WebUI.IntegrationTests.Common;
using System;
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
            await _factory.GetAuthenticatedClientAsync("User");
        }

        [Fact]
        public async Task GivenModerator_ReturnsJwtToken()
        {
            await _factory.GetAuthenticatedClientAsync("Moderator");
        }

        [Fact]
        public async Task GivenAdmin_ReturnsJwtToken()
        {
            await _factory.GetAuthenticatedClientAsync();
        }

        [Fact]
        public async Task GivenWrongRole_Return()
        {
            Exception ex = await Assert.ThrowsAsync<Exception>(async () => await _factory.GetAuthenticatedClientAsync("Random"));

            Assert.Equal("Specified Role not exists", ex.Message);
        }
    }
}
