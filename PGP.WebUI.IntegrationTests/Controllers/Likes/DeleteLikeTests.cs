using PGP.WebUI.IntegrationTests.Common;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Likes
{
    public class DeleteLikeTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public DeleteLikeTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
    }
}
