using Application.Tasks.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Tasks.EventHandlers
{
    public class TaskDeletedEventHandler : INotificationHandler<TaskDeletedEvent>
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
