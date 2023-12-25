using Application.Tasks.Commands.UpdateTask;
using AutoFixture;
using FluentAssertions;
using Infrastructure.Data;
using IntegrationTests.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;

namespace IntegrationTests.Tasks.Commands
{
    [Collection("Database collection")]
    public class UpdateTaskTest
    {
        private readonly WebApplicationFactoryFixture _fixture;
        private readonly Fixture _autoFixture;

        public UpdateTaskTest(WebApplicationFactoryFixture fixture)
        {
            _fixture = fixture;
            _autoFixture = new Fixture();
        }

        [Fact]
        public async Task UpdateTask_ShouldReturn204()
        {
            using (var scope = _fixture.Factory.Services.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var task = _autoFixture.Create<Domain.Entities.Task>();
                task.Id = 0;
                context.Tasks.Add(task);
                context.SaveChanges();

                int count = context.Tasks.Count();
                string updatedTitle = "Updated";
                var command = new UpdateTaskCommand(task.Id, updatedTitle);

                // Act
                var response = await _fixture.Client.PutAsJsonAsync("api/tasks/" + task.Id, command);
                context.Entry(task).State = EntityState.Detached;

                // Assert
                ((int)response.StatusCode).Should().Be(StatusCodes.Status204NoContent);
                context.Tasks.Count().Should().Be(count);

                var updatedTask = context.Tasks.Find(command.Id);
                updatedTask.Should().NotBeNull();
                updatedTask?.Title.Should().Be(updatedTitle);
            }
        }

        [Fact]
        public async Task UpdateTask_ShouldReturn404_WhenBadParameters()
        {
            using (var scope = _fixture.Factory.Services.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var task = _autoFixture.Create<Domain.Entities.Task>();
                task.Id = 0;
                context.Tasks.Add(task);
                context.SaveChanges();

                int count = context.Tasks.Count();
                int badId = task.Id + 100;
                string updatedTitle = "Updated";
                var command = new UpdateTaskCommand(task.Id, updatedTitle);

                // Act
                var response = await _fixture.Client.PutAsJsonAsync("api/tasks/" + badId, command);
                
                // Assert
                ((int)response.StatusCode).Should().Be(StatusCodes.Status400BadRequest);
                context.Tasks.Count().Should().Be(count);
            }
        }

        [Fact]
        public async Task UpdateTask_ShouldReturn400_WhenBadId()
        {
            using (var scope = _fixture.Factory.Services.CreateScope())
            {
                // Arrange
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var task = _autoFixture.Create<Domain.Entities.Task>();
                task.Id = 0;
                context.Tasks.Add(task);
                context.SaveChanges();

                int count = context.Tasks.Count();
                int badId = task.Id + 100;
                string updatedTitle = "Updated";
                var command = new UpdateTaskCommand(badId, updatedTitle);

                // Act
                var response = await _fixture.Client.PutAsJsonAsync("api/tasks/" + badId, command);

                // Assert
                ((int)response.StatusCode).Should().Be(StatusCodes.Status404NotFound);
                context.Tasks.Count().Should().Be(count);
            }
        }
    }
}
