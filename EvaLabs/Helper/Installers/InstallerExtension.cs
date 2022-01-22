using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Helper.Installers
{
    public static class InstallerExtension
    {
        private static readonly List<IInstaller> Installers =
            Assembly.GetExecutingAssembly().GetExportedTypes()
            .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .OrderBy(e => e.Order)
            .ToList();

        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            Installers.ForEach(installer =>
            {
                Console.WriteLine($"ConfigureService => :{installer.GetType().FullName}");
                installer.ConfigureServices(services, configuration);
            });
        }

        public static void ConfigurePipelineInAssembly(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            Installers.ForEach(installer =>
            {
                Console.WriteLine($"ConfigurePipeline => :{installer.GetType().FullName}");
                installer.Configure(app, env);
            });
        }
    }
}
