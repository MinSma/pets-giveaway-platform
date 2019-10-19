using PGP.Application.Comments.Commands.UpdateComment;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Comments
{
    public class UpdateCommentTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public UpdateCommentTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new UpdateCommentCommand
            {
                Text = "Comment text",
                UserId = 2
            };

            var content = Utilities.GetRequestContent(command);

            var validId = 1;

            var response = await client.PutAsync($"/api/comments/{validId}", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenAuthorizedUserIsNotCommentCreatorAndNotAdmin_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var command = new UpdateCommentCommand
            {
                Text = "Comment text",
                UserId = 4
            };

            var content = Utilities.GetRequestContent(command);

            var validId = 1;

            var response = await client.PutAsync($"/api/comments/{validId}", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidCommentId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var command = new UpdateCommentCommand
            {
                Text = "Comment text",
                UserId = 2
            };

            var content = Utilities.GetRequestContent(command);

            var invalidId = 10;

            var response = await client.PutAsync($"/api/comments/{invalidId}", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidData_ReturnOkStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var command = new UpdateCommentCommand
            {
                Text = "Comment text",
                UserId = 3
            };

            var content = Utilities.GetRequestContent(command);

            var validId = 1;

            var response = await client.PutAsync($"/api/comments/{validId}", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
