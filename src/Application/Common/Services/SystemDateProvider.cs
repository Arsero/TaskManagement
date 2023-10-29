using Domain.Interfaces;

namespace Application.Common.Services
{
    public class SystemDateProvider : IDateProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
