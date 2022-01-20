using FluentValidation;
using FluentValidation.Results;

namespace EvaLabs.ViewModels.Common.Helper
{
    public static class FluentValidatorExtension
    {
        public static ValidationResult CustomizedValidate<T>(this IValidator<T> validator, T instance)
        {
            return validator.Validate(instance);
        }
    }
}