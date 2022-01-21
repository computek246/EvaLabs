using EvaLabs.Helper.Attributes;
using EvaLabs.Helper.Implementations;
using EvaLabs.Helper.Installers;
using EvaLabs.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace EvaLabs.Helper.DI
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages();
            services.AddMvc(options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation();


            // Add framework services.
            services
                .AddControllersWithViews()
                // Maintain property names during serialization. See:
                // https://github.com/aspnet/Announcements/issues/194
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            // Add Kendo UI services to the services container
            services.AddKendo();

            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddTransient(typeof(ICurrentUserService<>), typeof(CurrentUserService<>));
            services.AddHttpContextAccessor();

            services.AddHealthChecks();
        }

        public int Order => 3;
    }
}
