using PGP.Application.Pets.Queries.GetPetById;
using PGP.WebUI.IntegrationTests.Common;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Pets
{
    public class GetPetByIdTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetPetByIdTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GivenValidPetId_ReturnPetModel()
        {
            var client = await _factory.GetAnonymousClient();
            
            var id = 1;
            
            var response = await client.GetAsync($"/api/pets/{id}");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<GetPetByIdQueryResponse>(response);

            Assert.IsType<GetPetByIdQueryResponse>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GivenInvalidPetId_ReturnsNotFoundStatusCode()
        {
            var client = await _factory.GetAnonymousClient();
            
            var invalidId = 10;
            
            var response = await client.GetAsync($"/api/pets/{invalidId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
