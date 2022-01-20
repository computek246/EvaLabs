using EvaLabs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Domain.Context
{
    public sealed partial class EvaContext : DbContext
    {

        public EvaContext(DbContextOptions<EvaContext> options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<AspNetUser> AspNetUsers { get; set; }
    }
}