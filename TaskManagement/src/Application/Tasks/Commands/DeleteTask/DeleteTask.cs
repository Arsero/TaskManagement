using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Tasks.Commands.DeleteTask
{
    public record DeleteTaskCommand(int Id) : IRequest;

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetById(request.Id)
                ?? throw new NotFoundException("Task not found.");

            await _taskRepository.Remove(task);
        }
    }
}
