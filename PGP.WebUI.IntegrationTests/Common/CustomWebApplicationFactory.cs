using IdentityModel.Client;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PGP.Application.Users.UserLogin;
using PGP.Persistence;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PGP.WebUI.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<PGPDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDatabaseForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddScoped<IPGPDbContext>(provider => provider.GetService<PGPDbContext>());

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<PGPDbContext>();

                    context.Database.EnsureCreated();

                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            })
            .UseEnvironment("Test");
        }

        public Task<HttpClient> GetAnonymousClient()
        {
            return Task.FromResult(CreateClient());
        }

        public async Task<HttpClient> GetAuthenticatedClientAsync(string role = "Admin")
        {
            var client = CreateClient();

            var command = new UserLoginCommand();

            switch (role)
            {
                case "Admin":
                    command.Email = "admin@admin.com";
                    command.Password = "password";
                    break;
                case "Moderator":
                    command.Email = "moderator@email.com";
                    command.Password = "password";
                    break;
                case "User":
                    command.Email = "petras@petrauskas.com";
                    command.Password = "password";
                    break;
                default:
                    throw new Exception("Specified Role not exists");
            }

            var content = Utilities.GetRequestContent(command);

            var response = await client.PostAsync($"/api/users/login", content);

            var responseContent = await Utilities.GetResponseContent<UserLoginCommandResponse>(response);

            client.SetBearerToken(responseContent.JwtToken);

            return client;
        }
    }
}
