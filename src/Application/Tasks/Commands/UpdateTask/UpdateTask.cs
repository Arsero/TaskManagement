using Application.Common.Interfaces;
using Application.Tasks.Events;
using Domain.Exceptions;
using MediatR;

namespace Application.Tasks.Commands.UpdateTask
{
    public record UpdateTaskCommand : IRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsCompleted { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPublisher _publisher;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository, IPublisher publisher)
        {
            this._taskRepository = taskRepository;
            this._publisher = publisher;
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
            await _publisher.Publish(new TaskDeletedEvent(task), cancellationToken);
        }
    }
}
