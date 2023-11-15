using Application.Common.Interfaces.Repository;
using Application.Tasks.Events;
using MediatR;
using IPublisher = Application.Common.Interfaces.Events.IPublisher;

namespace Application.Tasks.Commands.CreateTask
{
    public record CreateTaskCommand : IRequest<Domain.Entities.Task>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsCompleted { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Domain.Entities.Task>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IPublisher _publisher;

        public CreateTaskCommandHandler(ITaskRepository taskRepository, IPublisher publisher)
        {
            this._taskRepository = taskRepository;
            this._publisher = publisher;
        }

        public async Task<Domain.Entities.Task> Handle(CreateTaskCommand request, CancellationToken cancellationToken = default)
        {
            var task = new Domain.Entities.Task
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate ?? DateTime.Now,
                IsCompleted = request.IsCompleted ?? false
            };

            await _taskRepository.Add(task);
            await _publisher.Publish(new TaskCreatedEvent(task), cancellationToken);

            return await Task.FromResult(task);
        }
    }
}
