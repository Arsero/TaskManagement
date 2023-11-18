using Domain.UnitTests.Common;
using FluentAssertions;
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

            // Act
            task.Complete(new TestDateProvider(new DateTime(year, month, day)));

            // Assert
            task.IsCompleted.Should().Be(expected);
        }
    }
}