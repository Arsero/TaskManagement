using Application.Common.Interfaces.Events;
using Application.Tasks.Events;
using Microsoft.Extensions.Logging;

namespace Application.Tasks.EventHandlers
{
    public class TaskUpdatedEventHandler : IEventHandler<TaskUpdatedEvent>
    {
        private readonly ILogger<TaskUpdatedEventHandler> _logger;

        public TaskUpdatedEventHandler(ILogger<TaskUpdatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TaskUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{DomainEvent} : {@task}", notification.GetType().Name, notification.UpdatedTask);

            return Task.CompletedTask;
        }
    }
}
