using MediatR;
namespace Application.Tasks.Events
{
    public class TaskCompletedEvent : INotification
    {
        public Domain.Entities.Task CompletedTask { get; set; }

        public TaskCompletedEvent(Domain.Entities.Task completedTask)
        {
            CompletedTask = completedTask;
        }
    }
}
