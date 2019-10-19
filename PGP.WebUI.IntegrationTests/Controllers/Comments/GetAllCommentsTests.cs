using PGP.Application.Comments.Queries.GetAllComments;
using PGP.WebUI.IntegrationTests.Common;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Comments
{
    public class GetAllCommentsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAllCommentsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();
            
            var response = await client.GetAsync("/api/comments");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserIsNotAdmin_ReturnForbiddenStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");
            
            var response = await client.GetAsync("/api/comments");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserIsAdmin_ReturnNotEmptyList()
        {
            var client = await _factory.GetAuthenticatedClientAsync();
            
            var response = await client.GetAsync("/api/comments");

            var result = await Utilities.GetResponseContent<List<GetAllCommentsQueryResponse>>(response);

            Assert.IsType<List<GetAllCommentsQueryResponse>>(result);
            Assert.NotEmpty(result);
        }
    }
}
