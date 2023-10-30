using Application.Common.Interfaces.Events;
using Application.Tasks.Events;
using Microsoft.Extensions.Logging;

namespace Application.Tasks.EventHandlers
{
    public class TaskCreatedEventHandler : IEventHandler<TaskCreatedEvent>
    {
        private readonly ILogger<TaskCreatedEventHandler> _logger;

        public TaskCreatedEventHandler(ILogger<TaskCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TaskCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{DomainEvent} : {@task}", notification.GetType().Name, notification.CreatedTask);

            return Task.CompletedTask;
        }
    }
}
