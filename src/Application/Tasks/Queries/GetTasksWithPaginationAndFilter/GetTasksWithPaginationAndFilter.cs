using Application.Common.Interfaces.Repository;
using MediatR;

namespace Application.Tasks.Queries.GetTasksWithPaginationAndFilter
{
    public record GetTasksWithPaginationFilterAndSortingQuery(
        bool? FilterComplete, 
        bool? OrderByDate,
        int PageNumber = 1,
        int PageSize = 10
        ) : IRequest<IEnumerable<Domain.Entities.Task>>;

    public class GetTasksWithPaginationFilterAndSortingQueryHandler : IRequestHandler<GetTasksWithPaginationFilterAndSortingQuery, IEnumerable<Domain.Entities.Task>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksWithPaginationFilterAndSortingQueryHandler(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Task>> Handle(GetTasksWithPaginationFilterAndSortingQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetWithFilteringAndSorting(
                request.FilterComplete,
                request.OrderByDate,
                request.PageNumber,
                request.PageSize);

            return tasks;
        }
    }
}
