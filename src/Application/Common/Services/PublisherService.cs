using Application.Common.Interfaces.Events;

namespace Application.Common.Services
{
    public class PublisherService : Interfaces.Events.IPublisher
    {
        private readonly MediatR.IPublisher _publisher;

        public PublisherService(MediatR.IPublisher publisher)
        {
            this._publisher = publisher;
        }

        public async Task Publish<TEvent>(TEvent notification, CancellationToken cancellationToken = default) where TEvent : IEvent
        {
            await _publisher.Publish(notification, cancellationToken);
        }
    }
}
