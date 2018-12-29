using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	internal sealed class ValidationFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
			var provider = context.HttpContext.RequestServices;
			var options = provider.GetRequiredService<IOptions<ValidationOptions>>().Value;
			var formatter = provider.GetRequiredService<IValidationOutputFormatter>();
			
			var validationContext = provider.GetRequiredService<IValidationContext>();
			
			foreach (var model in context.ActionArguments.Values)
			{
				if (options.Validations.TryGetValue(model.GetType(), out var validation))
				{
					validation(provider, model);
				}
			}

			if (validationContext.Errors.Any(error => error.Severity >= validationContext.Severity))
			{
				context.Result = new ValidationResult(formatter.FormatOutput(validationContext.Errors));
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			var provider = context.HttpContext.RequestServices;
			
			var validationContext = provider.GetRequiredService<IValidationContext>();
			var formatter = provider.GetRequiredService<IValidationOutputFormatter>();

			if (validationContext.Errors.Any(error => error.Severity >= validationContext.Severity))
			{
				context.Result = new ValidationResult(formatter.FormatOutput(validationContext.Errors));
			}
		}
	}
}