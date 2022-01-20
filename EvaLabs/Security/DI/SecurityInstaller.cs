using EvaLabs.Helper;
using EvaLabs.Helper.ExtensionMethod;
using EvaLabs.Helper.Installers;
using EvaLabs.Security.Context;
using EvaLabs.Security.Entities;
using EvaLabs.Security.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvaLabs.Security.DI
{
    public class SecurityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<User, Role>()
                .AddClaimsPrincipalFactory<AdditionalUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

            services.AddScoped<IEmailSender, EmailSender>();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
            });

            services.GetService<ApplicationDbContext>(c => c.Database.Migrate());
        }

        public int Order => 2;
    }
}
