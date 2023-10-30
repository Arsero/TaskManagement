namespace Application.Common.Interfaces.Events
{
    public interface IPublisher
    {
        Task Publish<TEvent>(TEvent notification, CancellationToken cancellationToken = default)
        where TEvent : IEvent;
    }
}
