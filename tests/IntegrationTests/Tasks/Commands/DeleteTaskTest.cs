using AutoFixture;
using FluentAssertions;
using Infrastructure.Data;
using IntegrationTests.Common;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTests.Tasks.Commands
{
    [Collection("Database collection")]
    public class DeleteTaskTest
    {
        private readonly WebApplicationFactoryFixture _fixture;
        private readonly Fixture _autoFixture;

        public DeleteTaskTest(WebApplicationFactoryFixture fixture)
        {
            _fixture = fixture;
            _autoFixture = new Fixture();
        }

        [Fact]
        public async Task DeleteTask_ShouldReturn204()
        {
            using (var scope = _fixture.Factory.Services.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var task = ContextHelper.SeedTask(context);
                int count = context.Tasks.Count();

                // Act
                var response = await _fixture.Client.DeleteAsync("api/tasks/" + task.Id);

                // Assert
                ((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
                context.Tasks.Count().Should().Be(count - 1);
                context.Tasks.Find(task.Id).Should().BeNull();
            }
        }

        [Fact]
        public async Task DeleteTask_ShouldReturn404_WhenBadParameters()
        {
            using (var scope = _fixture.Factory.Services.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var task = ContextHelper.SeedTask(context);

                int count = context.Tasks.Count();
                int badId = task.Id + 1;

                // Act
                var response = await _fixture.Client.DeleteAsync("api/tasks/" + badId);

                // Assert
                ((int)response.StatusCode).Should().Be(StatusCodes.Status404NotFound);
                context.Tasks.Count().Should().Be(count);
                context.Tasks.Find(task.Id).Should().NotBeNull();
            }
        }
    }
}
