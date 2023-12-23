using Application.Common.Exceptions;
using Application.Common.Interfaces.Repository;
using MediatR;

namespace Application.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand(int Id, string? Title = null, string? Description = null, DateTime? DueDate = null, bool? IsCompleted = null) : IRequest;

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetById(request.Id)
                ?? throw new NotFoundException("Task not found.");

            task.Title = request.Title ?? task.Title;
            task.Description = request.Description ?? task.Description;
            task.DueDate = request.DueDate ?? task.DueDate;
            task.IsCompleted = request.IsCompleted ?? task.IsCompleted;

            await _taskRepository.Update(task);
        }
    }
}
