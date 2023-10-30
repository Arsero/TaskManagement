using Application.Common.Interfaces.Events;

namespace Application.Tasks.Events
{
    public class TaskUpdatedEvent : IEvent
    {
        public Domain.Entities.Task UpdatedTask { get; set; }

        public TaskUpdatedEvent(Domain.Entities.Task updatedTask)
        {
            UpdatedTask = updatedTask;
        }
    }
}
