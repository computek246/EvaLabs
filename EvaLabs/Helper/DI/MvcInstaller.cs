using EvaLabs.Helper.Implementations;
using EvaLabs.Helper.Installers;
using EvaLabs.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Helper.DI
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation();

            services.AddControllersWithViews();

            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddTransient(typeof(ICurrentUserService<>), typeof(CurrentUserService<>));
            services.AddHttpContextAccessor();

            services.AddHealthChecks();
        }

        public int Order => 3;
    }
}
