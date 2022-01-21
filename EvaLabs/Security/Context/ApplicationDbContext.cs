using System.Reflection;
using EvaLabs.Helper.ExtensionMethod;
using EvaLabs.Security.Configurations;
using EvaLabs.Security.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Security.Context
{
    public class ApplicationDbContext
        : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyAllConfigurationsFromCurrentAssembly(Assembly.GetExecutingAssembly(), typeof(UserConfiguration).Namespace);
        }
    }
}