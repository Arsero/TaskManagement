﻿using Application.Tasks.Commands.CreateTask;
using FluentAssertions;
using IntegrationTests.Database;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using Xunit;

namespace IntegrationTests.Tasks.Commands
{
    public class CreateTaskTest : BaseTest
    {
        public CreateTaskTest(DatabaseFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task CreateTask_ShouldReturn201()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Id = 0,
                Title = "Title",
                Description = "Description",
                DueDate = DateTime.Now.AddDays(1),
                IsCompleted = false,
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/tasks", command);

            // Assert
            ((int) response.StatusCode).Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        public async Task CreateTask_ShouldReturn400_WhenBadParameters()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Id = 0,
                Title = "Title",
                Description = null,
                DueDate = null,
                IsCompleted = false,
            };

            // Act
            var response = await _client.PostAsJsonAsync("api/tasks", command);

            // Assert
            ((int)response.StatusCode).Should().Be(StatusCodes.Status400BadRequest);
        }
    }
}
