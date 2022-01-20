using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Helper.Installers
{
    public static class InstallerExtension
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = Assembly.GetExecutingAssembly().GetExportedTypes()
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IInstaller>()
                .OrderBy(e => e.Order)
                .ToList();

            installers.ForEach(installer =>
            {
                Console.WriteLine($"InstallService => :{installer.GetType().FullName}");
                installer.InstallServices(services, configuration);
            });
        }
    }
}
