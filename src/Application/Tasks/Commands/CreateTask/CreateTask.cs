using Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<CreateTaskCommandHandler> _logger;

        public CreateTaskCommandHandler(ITaskRepository taskRepository, ILogger<CreateTaskCommandHandler> logger)
        {
            this._taskRepository = taskRepository;
            this._logger = logger;
        }

        public async Task<Domain.Entities.Task> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new Domain.Entities.Task
            {
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate ?? DateTime.Now,
                IsCompleted = request.IsCompleted ?? false
            };

            _logger.LogInformation("Task created : {@task}", task);

            await _taskRepository.Add(task);
            return await Task.FromResult(task);
        }
    }
}
