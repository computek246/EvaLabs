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

        public DbSet<User> Users { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Lab> Labs { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestStatus> TestStatuses { get; set; }
        public DbSet<UserTest> UserTests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<TestBranchs> TestBranchs { get; set; }

    }
}