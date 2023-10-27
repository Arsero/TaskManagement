using Application.Common.Interfaces;
using Application.Common.Services;
using Domain.Exceptions;
using MediatR;

namespace Application.Tasks.Commands.CompleteTask
{
    public record CompleteTaskCommand(int Id) : IRequest;

    public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public CompleteTaskCommandHandler(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetById(request.Id)
                ?? throw new NotFoundException("Task not found.");

            task.Complete(new SystemDateProvider());
            await _taskRepository.Update(task);
        }
    }
}
