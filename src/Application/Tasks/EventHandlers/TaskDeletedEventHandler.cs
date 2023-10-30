using Application.Common.Interfaces.Events;
using Application.Tasks.Events;
using Microsoft.Extensions.Logging;

namespace Application.Tasks.EventHandlers
{
    public class TaskDeletedEventHandler : IEventHandler<TaskDeletedEvent>
    {
        private readonly ILogger<TaskDeletedEventHandler> _logger;

        public TaskDeletedEventHandler(ILogger<TaskDeletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TaskDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{DomainEvent} : {@task}", notification.GetType().Name, notification.DeletedTask);

            return Task.CompletedTask;
        }
    }
}
