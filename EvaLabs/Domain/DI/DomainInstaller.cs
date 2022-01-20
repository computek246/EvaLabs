using EvaLabs.Domain.Context;
using EvaLabs.Helper.ExtensionMethod;
using EvaLabs.Helper.Installers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Domain.DI
{
    public class DomainInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EvaContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.GetService<EvaContext>(c => c.Database.Migrate());
        }

        public int Order => 1;
    }
}
