using System.Linq;
using EvaLabs.Domain.Context;
using EvaLabs.Domain.Entities;
using EvaLabs.Helper.ExtensionMethod;
using EvaLabs.Helper.Installers;
using EvaLabs.Infrastructure.SingletonFactory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Domain.DI
{
    public class DomainInstaller : IInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EvaContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });


            services.GetService<EvaContext>(context =>
            {
                if (context.Database.CanConnect())
                {
                    Singleton<Lab>.Instance = context.Labs.FirstOrDefault(e => e.IsActive);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public int Order => 1;
    }
}
