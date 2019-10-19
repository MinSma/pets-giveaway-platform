using PGP.WebUI.IntegrationTests.Common;
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
    }
}
