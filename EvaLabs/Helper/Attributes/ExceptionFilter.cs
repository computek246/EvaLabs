using System;
using System.Diagnostics;
using EvaLabs.ViewModels.Common;
using EvaLabs.ViewModels.Common.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EvaLabs.Helper.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionFilter : Attribute, IExceptionFilter, IActionFilter
    {
        public Stopwatch Stopwatch { get; set; }

        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception.ToString().Replace(Environment.NewLine, "\\n"));

            context.Result =
                new BadRequestObjectResult(
                    ResponseVm<string>.BadRequest(Result<string>.Failed(context.Exception.Message)));
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // do nothing
            _logger.LogInformation($"OnActionExecuting :{context.ActionDescriptor.DisplayName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do nothing
            _logger.LogInformation($"OnActionExecuted :{context.ActionDescriptor.DisplayName}");
        }
    }
}
