using Application.Common.Interfaces.Events;

namespace Application.Tasks.Events
{
    public class TaskCreatedEvent : IEvent
    {
        public Domain.Entities.Task CreatedTask { get; set; }

        public TaskCreatedEvent(Domain.Entities.Task createdTask)
        {
            this.CreatedTask = createdTask;
        }
    }
}
