using MediatR;

namespace Application.Tasks.Events
{
    public class TaskUpdatedEvent : INotification
    {
        public Domain.Entities.Task UpdatedTask { get; set; }

        public TaskUpdatedEvent(Domain.Entities.Task updatedTask)
        {
            UpdatedTask = updatedTask;
        }
    }
}
