using Employees.Shared.DTOs;

namespace Employees.Backend.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationDTO p)
            => query.Skip((p.Page - 1) * p.RecordsNumber).Take(p.RecordsNumber);
    }
}
