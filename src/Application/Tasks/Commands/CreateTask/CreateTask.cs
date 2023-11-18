using Application.Common.Interfaces.Repository;
using Domain.Events;
using MediatR;

namespace Application.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand : IRequest
    {
        public int Id { get; set; }
        public string? Title { get; init; }
        public string? Description { get; init; }
        public DateTime? DueDate { get; init; }
        public bool? IsCompleted { get; init; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task Handle(CreateTaskCommand request, CancellationToken cancellationToken = default)
        {
            var task = new Domain.Entities.Task
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate ?? DateTime.Now,
                IsCompleted = request.IsCompleted ?? false
            };

            await _taskRepository.Add(task);
            task.AddDomainEvent(new TaskCreatedEvent(task));

            request.Id = task.Id;
        }
    }
}
