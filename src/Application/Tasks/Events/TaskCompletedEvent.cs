using Application.Common.Interfaces.Events;

namespace Application.Tasks.Events
{
    public class TaskCompletedEvent : IEvent
    {
        public Domain.Entities.Task CompletedTask { get; set; }

        public TaskCompletedEvent(Domain.Entities.Task completedTask)
        {
            CompletedTask = completedTask;
        }
    }
}
