using Application.Tasks.Commands.UpdateTask;
using FluentAssertions;
using IntegrationTests.Database;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using Xunit;

namespace IntegrationTests.Tasks.Commands
{
    public class UpdateTaskTest : BaseTest
    {
        public UpdateTaskTest(DatabaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task UpdateTask_ShouldReturn204()
        {
            int id = 1;

            // Arrange
            var command = new UpdateTaskCommand(id, "updated");

            // Act
            var response = await _client.PutAsJsonAsync("api/tasks/" + id,  command);

            // Assert
            ((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
        }

        [Fact]
        public async Task UpdateTask_ShouldReturn404_WhenBadParameters()
        {
            // Arrange
            int id = 300;
            var command = new UpdateTaskCommand(id, "updated");

            // Act
            var response = await _client.PutAsJsonAsync("api/tasks/" + id, command);

            // Assert
            ((int)response.StatusCode).Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task UpdateTask_ShouldReturn400_WhenBadId()
        {
            // Arrange
            int id = 1, badId = 2;
            var command = new UpdateTaskCommand(id, "updated");

            // Act
            var response = await _client.PutAsJsonAsync("api/tasks/" + badId, command);

            // Assert
            ((int)response.StatusCode).Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
