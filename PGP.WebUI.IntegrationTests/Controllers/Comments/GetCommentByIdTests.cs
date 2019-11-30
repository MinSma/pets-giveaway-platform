using PGP.Application.Comments.Queries.GetCommentById;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Comments
{
    public class GetCommentByIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetCommentByIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validId = 1;

            var response = await client.GetAsync($"/api/comments/{validId}");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserIsNotAdmin_ReturnForbiddenStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var validId = 1;

            var response = await client.GetAsync($"/api/comments/{validId}");

            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserIsAdminButInvalidCommentId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var invalidId = 10;

            var response = await client.GetAsync($"/api/comments/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserIsAdminAndValidCommentId_ReturnOkStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var validId = 1;

            var response = await client.GetAsync($"/api/comments/{validId}");

            var result = await ClientUtilities.GetResponseContent<GetCommentByIdQueryResponse>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<GetCommentByIdQueryResponse>(result);
        }
    }
}
