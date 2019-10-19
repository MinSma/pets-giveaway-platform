using PGP.Application.Categories.Queries.GetAllCategories;
using PGP.Application.Likes.Queries.GetAllLikedPetsByUserId;
using PGP.WebUI.IntegrationTests.Common;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Likes
{
    public class GetAllLikedPetsByUserId : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAllLikedPetsByUserId(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenNotAutenticatedUser_ReturnUnauthorizedStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validUserId = 1;

            var response = await client.GetAsync($"/api/users/{validUserId}/likes");

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GivenInvalidUserId_ReturnEmptyList()
        {
            var client = await _factory.GetAuthenticatedClientAsync();

            var invalidUserId = 10;

            var response = await client.GetAsync($"/api/users/{invalidUserId}/likes");

            var result = await Utilities.GetResponseContent<List<GetAllLikedPetsByUserIdQueryResponse>>(response);

            Assert.IsType<List<GetAllLikedPetsByUserIdQueryResponse>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GivenValidUserId_ReturnNotEmptyList()
        {
            var client = await _factory.GetAuthenticatedClientAsync("User");

            var valid = 3;

            var response = await client.GetAsync($"/api/users/{valid}/likes");

            var result = await Utilities.GetResponseContent<List<GetAllLikedPetsByUserIdQueryResponse>>(response);

            Assert.IsType<List<GetAllLikedPetsByUserIdQueryResponse>>(result);
            Assert.NotEmpty(result);
        }
    }
}
