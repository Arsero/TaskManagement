using AutoFixture;
using FluentAssertions;
using Infrastructure.Data;
using IntegrationTests.Common;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;

namespace IntegrationTests.Tasks.Queries
{
    [Collection("Database collection")]
    public class GetTaskTest
    {
        private readonly WebApplicationFactoryFixture _fixture;
        private readonly Fixture _autoFixture;

        public GetTaskTest(WebApplicationFactoryFixture fixture)
        {
            _fixture = fixture;
            _autoFixture = new Fixture();
        }

        [Fact]
        public async Task GetTaskById_ShouldReturn200()
        {
            using (var scope = _fixture.Factory.Services.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var task = ContextHelper.SeedTask(context);

                // Act
                var response = await _fixture.Client.GetAsync("api/tasks/" + task.Id);

                // Assert
                ((int)response.StatusCode).Should().Be(StatusCodes.Status200OK);
                var responseTask = await response.Content
                    .ReadFromJsonAsync<Domain.Entities.Task>();

                responseTask.Should().NotBeNull();
                responseTask?.Id.Should().Be(task.Id);
                responseTask?.Title.Should().Be(task.Title);
            }
        }

        [Fact]
        public async Task GetTaskById_ShouldReturn404_WhenBadId()
        {
            using (var scope = _fixture.Factory.Services.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var task = ContextHelper.SeedTask(context);
                int badId = task.Id + 100;

                // Act
                var response = await _fixture.Client.GetAsync("api/tasks/" + badId);

                // Assert
                ((int)response.StatusCode).Should().Be(StatusCodes.Status404NotFound);
            }
        }
    }
}
