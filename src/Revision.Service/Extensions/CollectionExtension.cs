using Revision.Domain.Configurations;

namespace Revision.Service.Extensions;

public static class CollectionExtension
{
    public static IQueryable<T> ToPaginate<T>(this IQueryable<T> values, PaginationParams pagination)
        => values.Skip((pagination.PageIndex - 1) * pagination.PageSize).Take(pagination.PageSize);
}
