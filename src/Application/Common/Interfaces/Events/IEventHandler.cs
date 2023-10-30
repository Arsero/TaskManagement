using MediatR;

namespace Application.Common.Interfaces.Events
{
    public interface IEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
    {
        new Task Handle(TEvent notification, CancellationToken cancellationToken = default);
    }
}
