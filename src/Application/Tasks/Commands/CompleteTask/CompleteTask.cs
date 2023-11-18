using Application.Common.Exceptions;
using Application.Common.Interfaces.Repository;
using Application.Common.Services;
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

            if (task.IsCompleted)
                throw new ValidationException("Task already completed.");

            task.Complete(new SystemDateProvider());

            if (!task.IsCompleted)
                throw new InvalidDateException("Task can only be complete on Thursday.");

            await _taskRepository.Update(task);
        }
    }
}
