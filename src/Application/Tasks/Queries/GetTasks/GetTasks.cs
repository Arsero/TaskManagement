using Application.Common.Interfaces;
using MediatR;

namespace Application.Tasks.Queries.GetTasks
{
    public record GetTasksQuery : IRequest<IEnumerable<Domain.Entities.Task>>
    { }

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<Domain.Entities.Task>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksQueryHandler(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Task>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskRepository.GetAll();
        }
    }
}
