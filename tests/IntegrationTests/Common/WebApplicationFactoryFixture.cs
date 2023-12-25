using Ardalis.GuardClauses;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace IntegrationTests.Common
{
    public class WebApplicationFactoryFixture : IAsyncLifetime
    {
        public WebApplicationFactory<Program> Factory { get; }

        public HttpClient Client { get; private set; }
        public WebApplicationFactoryFixture()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            Guard.Against.Null(connectionString);

            Factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(Services =>
                {
                    Services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
                    Services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(connectionString);
                    });
                });
            });
            Client = Factory.CreateClient();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            using (var scope = Factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();

                await context.Database.EnsureDeletedAsync();
                // await context.Tasks.ExecuteDeleteAsync();
            }
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            using (var scope = Factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var context = scopedServices.GetRequiredService<ApplicationDbContext>();

                await context.Database.EnsureCreatedAsync();
            }
        }
    }
}
