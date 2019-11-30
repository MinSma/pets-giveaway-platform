using PGP.Application.Roles.Queries.GetRoles;
using PGP.WebUI.IntegrationTests.Common;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Roles
{
    public class GetAllRolesTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAllRolesTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenAdminUser_ReturnSuccessResult()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            var response = await client.GetAsync("/api/roles");

            response.EnsureSuccessStatusCode();

            var result = await ClientUtilities.GetResponseContent<List<GetAllRolesQueryResponse>>(response);

            Assert.IsType<List<GetAllRolesQueryResponse>>(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GivenNotAdminUser_ReturnSuccessResult()
        {
            var client = await _factory.GetAnonymousClient();
            var response = await client.GetAsync("/api/roles");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
