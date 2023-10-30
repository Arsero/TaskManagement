using MediatR;

namespace Application.Tasks.Events
{
    public class TaskDeletedEvent : INotification
    {
        public Domain.Entities.Task DeletedTask { get; set; }

        public TaskDeletedEvent(Domain.Entities.Task deletedTask)
        {
            DeletedTask = deletedTask;
        }
    }
}
