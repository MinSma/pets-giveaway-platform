using PGP.Application.Pets.Queries.GetAllCommentsByPetId;
using PGP.WebUI.IntegrationTests.Common;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Pets
{
    public class GetAllCommentsByPetIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAllCommentsByPetIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenInvalidPetId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var invalidId = 10;

            var response = await client.GetAsync($"/api/pets/{invalidId}/comments");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task GivenValidPetId_ReturnNotFoundStatusCode()
        {
            var client = await _factory.GetAnonymousClient();

            var validId = 1;

            var response = await client.GetAsync($"/api/pets/{validId}/comments");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<List<GetAllCommentsByPetIdQueryResponse>>(response);

            Assert.IsType<List<GetAllCommentsByPetIdQueryResponse>>(result);
            Assert.NotEmpty(result);
        }
    }
}
