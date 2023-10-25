using Domain.Exceptions;

namespace Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public void Complete()
        {
            IsCompleted = DateTime.Now.DayOfWeek == DayOfWeek.Thursday 
                ? true
                : throw new ValidationException("Task can only be complete on Thursday.");
        }
    }
}
