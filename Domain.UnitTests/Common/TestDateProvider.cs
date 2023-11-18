using Domain.Interfaces;

namespace Domain.UnitTests.Common
{
    public class TestDateProvider : IDateProvider
    {
        public TestDateProvider(DateTime dateTime)
        {
            Now = dateTime;
        }

        public DateTime Now { get; init; }
    }
}
