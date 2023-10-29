namespace Application.Common.Pagination
{
    public static class PaginationExtension
    {
        public static PaginatedList<TDestination> ToPaginatedList<TDestination>(
                this IEnumerable<TDestination> list,
                int pageNumber,
                int pageSize
            ) where TDestination : class
            => PaginatedList<TDestination>.Create(list, pageNumber, pageSize);
    }
}
