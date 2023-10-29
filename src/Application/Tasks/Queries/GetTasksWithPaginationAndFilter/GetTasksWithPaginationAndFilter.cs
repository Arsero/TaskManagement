using Application.Common.Interfaces;
using Application.Common.Pagination;
using MediatR;

namespace Application.Tasks.Queries.GetTasksWithPaginationAndFilter
{
    public record GetTasksWithPaginationFilterAndSortingQuery : IRequest<IEnumerable<Domain.Entities.Task>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public bool? FilterComplete { get; init; }
        public bool? OrderByDate { get; init; }
    }

    public class GetTasksWithPaginationFilterAndSortingQueryHandler : IRequestHandler<GetTasksWithPaginationFilterAndSortingQuery, IEnumerable<Domain.Entities.Task>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksWithPaginationFilterAndSortingQueryHandler(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Task>> Handle(GetTasksWithPaginationFilterAndSortingQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetWithFilteringAndSorting(request.FilterComplete, request.OrderByDate);

            return tasks.ToPaginatedList(request.PageNumber, request.PageSize).Items;
        }
    }
}
