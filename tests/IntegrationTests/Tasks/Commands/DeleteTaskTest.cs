using Application.Tasks.Commands.CreateTask;
using Application.Tasks.Commands.DeleteTask;
using Application.Tasks.Commands.UpdateTask;
using FluentAssertions;
using IntegrationTests.Database;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using Xunit;

namespace IntegrationTests.Tasks.Commands
{
    public class DeleteTaskTest : BaseTest
    {
        public DeleteTaskTest(DatabaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task DeleteTask_ShouldReturn204()
        {
            // Arrange
            int id = 2;

            // Act
            var response = await _client.DeleteAsync("api/tasks/" + id);

            // Assert
            ((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task DeleteTask_ShouldReturn404_WhenBadParameters()
        {
            // Arrange
            int id = 1;

            // Act
            var response = await _client.DeleteAsync("api/tasks/" + id);

            // Assert
            ((int)response.StatusCode).Should().Be(StatusCodes.Status404NotFound);
        }
    }
}
