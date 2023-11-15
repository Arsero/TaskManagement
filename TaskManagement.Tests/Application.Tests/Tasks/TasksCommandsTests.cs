using Application.Common.Interfaces.Events;
using Application.Common.Interfaces.Repository;
using Application.Tasks.Commands.CreateTask;
using AutoFixture.AutoMoq;
using AutoFixture;
using FluentAssertions;
using Moq;
using TaskManagement.Tests.Common;

namespace TaskManagement.Tests.Application.Tests.Tasks
{
    public class TasksCommandsTests : IClassFixture<DatabaseFixture>
    {
        private readonly Mock<IPublisher> _mockPublisher;
        private readonly DatabaseFixture _databaseFixture;
        private readonly ITaskRepository _taskRepository;
        private readonly IFixture _fixture;

        public TasksCommandsTests(DatabaseFixture databaseFixture) 
        {
            _mockPublisher = new Mock<IPublisher>();
            _databaseFixture = databaseFixture;
            _taskRepository = _databaseFixture.TaskRepository;
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public async Task TasksService_CreateTask_ReturnTask()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Title = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                IsCompleted = _fixture.Create<bool>(),
                DueDate = _fixture.Create<DateTime>()
            };

            var handler = new CreateTaskCommandHandler(_taskRepository, _mockPublisher.Object);
            var countBefore = _taskRepository.Count();

            // Act
            var result = await handler.Handle(command);

            // Assert
            _taskRepository.Count().Should().Be(countBefore + 1);
        }
    }
}
