using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Services.BaseService
{
    public static class ServiceExtensions
    {
        public static void RegisterAllServicesByType(this IServiceCollection services)
        {

            Assembly.GetExecutingAssembly()
                .GetExportedTypes()
                .Where(x => typeof(IService).IsAssignableFrom(x))
                .Where(x => !x.IsAbstract && !x.IsGenericType && x.IsClass)
                .Select(x => new
                {
                    Interface = x.GetInterfaces().Last(),
                    Implementation = x
                })
                .ToList()
                .ForEach(x => services.AddScoped(x.Interface, x.Implementation));

        }
    }
}
