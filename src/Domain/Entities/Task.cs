using Domain.Common;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Task : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public void Complete(IDateProvider dateProvider)
        {
            if(dateProvider.Now.DayOfWeek == DayOfWeek.Thursday)
            {
                IsCompleted = true;
            }
        }
    }
}
