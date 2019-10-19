using PGP.Application.Comments.Commands.CreateComment;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Comments
{
    public class CreateCommentTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public CreateCommentTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenUnauthorizedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var command = new CreateCommentCommand
            {
                PetId = 1,
                UserId = 1,
                Text = "Comment text"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/api/comments", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenUserTryingCreateCommentInsteadOfOtherUser_ReturnsUnauthorizedStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("Moderator");

            var command = new CreateCommentCommand
            {
                PetId = 1,
                UserId = 4,
                Text = "Comment text"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/api/comments", content);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenNotExistingUserId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var command = new CreateCommentCommand
            {
                PetId = 1,
                UserId = 10,
                Text = "Comment text"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/api/comments", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenNotExistingPetId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var command = new CreateCommentCommand
            {
                PetId = 10,
                UserId = 3,
                Text = "Comment text"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/api/comments", content);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidData_ReturnOkStatusCode()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var command = new CreateCommentCommand
            {
                PetId = 1,
                UserId = 3,
                Text = "Comment text"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync("/api/comments", content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
