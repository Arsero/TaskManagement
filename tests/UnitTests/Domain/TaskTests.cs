using Domain.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Domain.UnitTests
{
    public class TaskTests
    {
        [Theory]
        [InlineData(16, 11, 2023, true)]
        [InlineData(17, 11, 2023, false)]
        [InlineData(15, 11, 2023, false)]
        [InlineData(23, 11, 2023, true)]
        public void Complete_True_OnlyOnThursday(int day, int month, int year, bool expected)
        {
            // Arrange
            Entities.Task task = new();

            var mockDateProvider = new Mock<IDateProvider>();
            mockDateProvider.Setup(x => x.Now).Returns(new DateTime(year, month, day));

            // Act
            task.Complete(mockDateProvider.Object);

            // Assert
            task.IsCompleted.Should().Be(expected);
        }
    }
}
