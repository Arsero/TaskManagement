using MediatR;

namespace Application.Tasks.Events
{
    public class TaskCreatedEvent : INotification
    {
        public Domain.Entities.Task CreatedTask { get; set; }

        public TaskCreatedEvent(Domain.Entities.Task createdTask)
        {
            this.CreatedTask = createdTask;
        }
    }
}
