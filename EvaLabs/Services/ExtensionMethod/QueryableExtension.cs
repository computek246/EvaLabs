using System.Linq;

namespace EvaLabs.Services.ExtensionMethod
{
    public static class QueryableExtension
    {
        public static IQueryable<TModel> ToPageList<TModel>(this IQueryable<TModel> queryable, int pageIndex = 0,
            int pageSize = 20)
        {
            return queryable.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}