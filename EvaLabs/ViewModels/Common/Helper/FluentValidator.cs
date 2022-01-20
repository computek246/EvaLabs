using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace EvaLabs.ViewModels.Common.Helper
{
    public static class FluentValidator
    {
        private const string InvalidData = "Invalid data";

        public static Result<bool> ModelValidator(this ValidationResult validate)
        {
            if (validate.IsValid) return Result<bool>.Success(true);
            var error = validate.Errors.FirstOrDefault();
            if (error == null)
                return Result<bool>.Failed(InvalidData);

            var checkConvert = int.TryParse(error.ErrorCode, out _);
            return Result<bool>.Failed(checkConvert ? error.ErrorMessage : InvalidData);
        }


        public static List<string> ModelValidatorList(this ValidationResult validate)
        {
            if (validate.IsValid) return new List<string>();
            var errorList = validate.Errors;
            if (errorList == null || !errorList.Any())
                return Result<bool>.Failed(InvalidData).Messages;

            return errorList.Select(c => c.ErrorMessage).ToList();
        }
    }
}