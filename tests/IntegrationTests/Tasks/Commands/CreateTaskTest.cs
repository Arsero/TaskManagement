using Application.Tasks.Commands.CreateTask;
using AutoFixture;
using FluentAssertions;
using Infrastructure.Data;
using IntegrationTests.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;

namespace IntegrationTests.Tasks.Commands
{
    [Collection("Database collection")]
    public class CreateTaskTest
    {
        private readonly WebApplicationFactoryFixture _fixture;
        private readonly Fixture _autoFixture;

        public CreateTaskTest(WebApplicationFactoryFixture fixture)
        {
            _fixture = fixture;
            _autoFixture = new Fixture();
        }

        [Fact]
        public async Task CreateTask_ShouldReturn201()
        {
            using (var scope = _fixture.Factory.Services.CreateScope())
            {
                // Arrange
                var command = _autoFixture.Create<CreateTaskCommand>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                int count = context.Tasks.Count();

                // Act
                var response = await _fixture.Client.PostAsJsonAsync("api/tasks", command);

                // Assert
                ((int) response.StatusCode).Should().Be(StatusCodes.Status201Created);
                context.Tasks.Count().Should().Be(count + 1);
            }
        }

        [Fact]
        public async Task CreateTask_ShouldReturn400_WhenBadParameters()
        {
            using (var scope = _fixture.Factory.Services.CreateScope())
            {
                // Arrange
                var command = new CreateTaskCommand { Id = 0, Title = null };
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                int count = context.Tasks.Count();

                // Act
                var response = await _fixture.Client.PostAsJsonAsync("api/tasks", command);

                // Assert
                ((int)response.StatusCode).Should().Be(StatusCodes.Status400BadRequest);
                count.Should().Be(context.Tasks.Count());
            }
        }
    }
}
