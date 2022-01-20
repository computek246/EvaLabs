using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace EvaLabs.Helper.ExtensionMethod
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces()
                .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var configurationInstance in typesToRegister.Select(Activator.CreateInstance))
            {
                modelBuilder.ApplyConfiguration((dynamic)configurationInstance);
            }
        }

        public static void ApplyAllConfigurationsFromCurrentAssembly(this ModelBuilder modelBuilder, Assembly assembly, string configNamespace = "")
        {
            Console.WriteLine($"ApplyAllConfigurationsFrom => :{configNamespace}");

            var applyGenericMethods = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);
            var applyGenericApplyConfigurationMethods = applyGenericMethods.Where(m => m.IsGenericMethod && m.Name.Equals("ApplyConfiguration", StringComparison.OrdinalIgnoreCase));
            var applyGenericMethod = applyGenericApplyConfigurationMethods.FirstOrDefault(m => m.GetParameters().FirstOrDefault()?.ParameterType.Name == "IEntityTypeConfiguration`1");


            var applicableTypes = assembly
                .GetTypes()
                .Where(c => c.IsClass && !c.IsAbstract && !c.ContainsGenericParameters);

            if (!string.IsNullOrEmpty(configNamespace))
            {
                applicableTypes = applicableTypes.Where(c => c.Namespace == configNamespace);
            }

            foreach (var type in applicableTypes)
            {
                foreach (var inter in type.GetInterfaces())
                {
                    // if type implements interface IEntityTypeConfiguration<SomeEntity>
                    if (!inter.IsConstructedGenericType ||
                        inter.GetGenericTypeDefinition() != typeof(IEntityTypeConfiguration<>)) continue;

                    // make concrete ApplyConfiguration<SomeEntity> method
                    if (applyGenericMethod is not null)
                    {
                        var applyConcreteMethod = applyGenericMethod.MakeGenericMethod(inter.GenericTypeArguments[0]);
                        // and invoke that with fresh instance of your configuration type
                        applyConcreteMethod.Invoke(modelBuilder, new[] { Activator.CreateInstance(type) });
                    }

                    Console.WriteLine("applied model " + type.Name);
                    break;
                }
            }
        }
    }
}
