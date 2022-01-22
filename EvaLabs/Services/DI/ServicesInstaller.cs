using EvaLabs.Domain.Context;
using EvaLabs.Helper.ExtensionMethod;
using EvaLabs.Helper.Installers;
using EvaLabs.Infrastructure;
using EvaLabs.Services.BaseService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Services.DI
{
    public class ServicesInstaller : IInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddUnitOfWork<EvaContext>();
            services.RegisterAllServicesByType();

            services.GetService<EvaContext>(x =>
            {
                //SingletonList<City>.Instance = x.Cities.ToList();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public int Order => 5;
    }
}
