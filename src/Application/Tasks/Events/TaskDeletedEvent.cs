using Application.Common.Interfaces.Events;

namespace Application.Tasks.Events
{
    public class TaskDeletedEvent : IEvent
    {
        public Domain.Entities.Task DeletedTask { get; set; }

        public TaskDeletedEvent(Domain.Entities.Task deletedTask)
        {
            DeletedTask = deletedTask;
        }
    }
}
