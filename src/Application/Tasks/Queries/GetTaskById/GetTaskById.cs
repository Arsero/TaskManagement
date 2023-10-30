using Application.Common.Interfaces.Repository;
using Domain.Exceptions;
using MediatR;

namespace Application.Tasks.Queries.GetTaskById
{
    public record GetTaskByIdQuery(int Id) : IRequest<Domain.Entities.Task>;

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, Domain.Entities.Task>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task<Domain.Entities.Task> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetById(request.Id)
                ?? throw new NotFoundException("Task not found.");

            return task;
        }
    }
}
