using PGP.Application.Users.Queries.GetAllUsers;
using PGP.WebUI.IntegrationTests.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Users
{
    public class GetAllUsersTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAllUsersTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnSuccessResult()
        {
            var client = await _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/users");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<List<GetAllUsersQueryResponse>>(response);

            Assert.IsType<List<GetAllUsersQueryResponse>>(result);
            Assert.NotEmpty(result);
        }
    }
}
