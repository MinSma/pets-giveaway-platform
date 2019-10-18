using PGP.Application.Users.Queries.GetUserById;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Users
{
    public class GetUserByIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetUserByIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenId_ReturnUserModel()
        {
            var client = await _factory.GetAnonymousClient();
            var id = 1;
            var response = await client.GetAsync($"/api/users/{id}");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<GetUserByIdQueryResponse>(response);

            Assert.IsType<GetUserByIdQueryResponse>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GivenInvalidId_ReturnsNotFoundStatusCode()
        {
            var client = await _factory.GetAnonymousClient();
            var invalidId = 10;
            var response = await client.GetAsync($"/api/users/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
