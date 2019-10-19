using PGP.WebUI.IntegrationTests.Common;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Pets
{
    public class DeletePetTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeletePetTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
    }
}
