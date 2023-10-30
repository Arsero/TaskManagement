using Application.Common.Interfaces.Events;
using Application.Tasks.Events;
using Microsoft.Extensions.Logging;

namespace Application.Tasks.EventHandlers
{
    public class TaskCompletedEventHandler : IEventHandler<TaskCompletedEvent>
    {
        private readonly ILogger<TaskCompletedEventHandler> _logger;

        public TaskCompletedEventHandler(ILogger<TaskCompletedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(TaskCompletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{DomainEvent} : {@task}", notification.GetType().Name, notification.CompletedTask);

            return Task.CompletedTask;
        }
    }
}
