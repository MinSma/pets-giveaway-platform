using PGP.Application.Categories.Queries.GetAllCategories;
using PGP.WebUI.IntegrationTests.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PGP.WebUI.IntegrationTests.Controllers.Categories
{
    public class GetAllCategoriesTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public GetAllCategoriesTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ReturnSuccessResult()
        {
            var client = await _factory.GetAnonymousClient();

            var response = await client.GetAsync("/api/categories");

            response.EnsureSuccessStatusCode();

            var result = await Utilities.GetResponseContent<List<GetAllCategoriesByUserIdQueryResponse>>(response);

            Assert.IsType<List<GetAllCategoriesByUserIdQueryResponse>>(result);
            Assert.NotEmpty(result);
        }
    }
}
