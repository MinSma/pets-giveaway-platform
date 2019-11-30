using PGP.Application.Pets.Queries.GetAllPets;
using PGP.WebUI.IntegrationTests.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Pets
{
    public class GetAllPetsTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAllPetsTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnSuccessResult()
        {
            var client = await _factory.GetAnonymousClient();
            
            var response = await client.GetAsync("/api/pets");

            response.EnsureSuccessStatusCode();

            var result = await ClientUtilities.GetResponseContent<List<GetAllPetsQueryResponse>>(response);

            Assert.IsType<List<GetAllPetsQueryResponse>>(result);
            Assert.NotEmpty(result);
        }
    }
}
