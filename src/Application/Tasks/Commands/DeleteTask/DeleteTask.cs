using Application.Common.Interfaces.Repository;
using Application.Tasks.Events;
using Domain.Exceptions;
using MediatR;
using IPublisher = Application.Common.Interfaces.Events.IPublisher;

namespace Application.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(int Id) : IRequest;

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPublisher _publisher;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository, IPublisher publisher)
        {
            this._taskRepository = taskRepository;
            this._publisher = publisher;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetById(request.Id)
                ?? throw new NotFoundException("Task not found.");

            await _taskRepository.Remove(task);
            await _publisher.Publish(new TaskDeletedEvent(task), cancellationToken);
        }
    }
}
