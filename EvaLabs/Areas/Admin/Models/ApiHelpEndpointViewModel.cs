using System.Collections.Generic;
using System.Reflection;

namespace EvaLabs.Areas.Admin.Models
{
    public class ApiHelpEndpointViewModel
    {
        public string Endpoint { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Area { get; set; }
        public string DisplayableName { get; set; }
        public string Description { get; set; }
        public string EndpointRoute => $"/{Endpoint}";
        public PropertyInfo[] Properties { get; set; }
        public List<IList<CustomAttributeTypedArgument>> PropertyDescription { get; set; }
        public List<string> Attributes { get; set; }
        public string ReturnType { get; set; }
    }
}
