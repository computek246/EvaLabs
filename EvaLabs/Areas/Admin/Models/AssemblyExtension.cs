using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EvaLabs.Helper.ExtensionMethod;

namespace EvaLabs.Areas.Admin.Models
{
    public static class AssemblyExtension
    {
        public static IEnumerable<MethodInfo> GetAssemblyMethodInfo<T>(this Assembly assembly)
        {
            return assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList()
                .SelectMany(type =>
                    type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute),
                    true).Any());
        }

        public static ApiHelpEndpointViewModel ToMethodInfo(this MethodInfo methodInfo)
        {
            if (methodInfo == null) return new ApiHelpEndpointViewModel();

            var declaringType = methodInfo.DeclaringType;
            if (declaringType == null) return new ApiHelpEndpointViewModel();

            var ns = declaringType.Namespace;
            ns = !string.IsNullOrEmpty(ns) && ns.Contains("Areas")
                ? ns.Split("Areas").Last().Replace("Controllers", "").Replace(".", "").Trim()
                : "";

            var controller = declaringType.Name.Replace("Controller", "");
            return new ApiHelpEndpointViewModel
            {
                Area = ns,
                Controller = controller,
                Action = methodInfo.Name,
                ReturnType = methodInfo.ReturnType.CSharpName(),
                DisplayableName = controller.AddSpacesToSentence(),
                Attributes = methodInfo.GetCustomAttributes().Select(s => s.GetType().Name.Replace("Attribute", ""))
                    .ToList()
            };
        }
    }
}
