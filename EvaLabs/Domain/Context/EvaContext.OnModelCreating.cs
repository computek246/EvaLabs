using System.Linq;
using System.Reflection;
using EvaLabs.Domain.Entities;
using EvaLabs.Helper.ExtensionMethod;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Domain.Context
{
    public sealed partial class EvaContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly(Assembly.GetExecutingAssembly(), typeof(AspNetUser).Namespace);

            foreach (var property in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                {
                    property.SetMaxLength(256);
                }
            }
        }
    }
}