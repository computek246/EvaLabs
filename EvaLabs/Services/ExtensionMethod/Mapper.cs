using System;
using System.Linq;

namespace EvaLabs.Services.ExtensionMethod
{
    public static class Mapper
    {
        public static TResult Map<TModel, TResult>(TModel modelObject)
        {
            var resultProperties = typeof(TResult).GetProperties();
            var modelProperties = modelObject.GetType().GetProperties();

            var result = (TResult)Activator.CreateInstance(typeof(TResult));

            foreach (var currentItem in resultProperties)
            {
                var name = currentItem.Name;
                var propertyType = currentItem.PropertyType;

                if (!modelProperties.Any(s => s.Name == name && s.PropertyType == propertyType)) continue;
                
                {
                    var value = modelProperties.Where(s => s.Name == name && s.PropertyType == propertyType)
                        .Select(s => s.GetValue(modelObject)).FirstOrDefault();

                    currentItem.SetValue(result, Convert.ChangeType(value, propertyType), null);
                }
            }

            return result;
        }
        
    }
}
