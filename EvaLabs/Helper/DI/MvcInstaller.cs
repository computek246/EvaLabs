using EvaLabs.Helper.Attributes;
using EvaLabs.Helper.Implementations;
using EvaLabs.Helper.Installers;
using EvaLabs.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace EvaLabs.Helper.DI
{
    public class MvcInstaller : IInstaller
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages();
            services.AddMvc(options =>
                {
                    options.Filters.Add<ExceptionFilter>();
                    options.EnableEndpointRouting = true;
                    
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation();

            services
                .AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.AddKendo();

            services.AddSingleton<IDateTimeService, DateTimeService>();
            services.AddTransient(typeof(ICurrentUserService<>), typeof(CurrentUserService<>));
            services.AddHttpContextAccessor();

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleWares(env);
        }

        public int Order => 3;
    }
}
