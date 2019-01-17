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
			
			var formatter = provider.GetRequiredService<IValidationOutputFormatter>();
			var validationContext = provider.GetRequiredService<IValidationContext>();
			var options = provider.GetRequiredService<IOptions<ValidationComponentOptions>>().Value;

			foreach (var model in context.ActionArguments.Values)
			{
				if (options.ValidationDispatchers.TryGetValue(model.GetType(), out var dispatchers))
				{
					foreach (var dispatcher in dispatchers)
					{
						dispatcher(provider, model);
					}
				}
			}

			var errors = validationContext.Errors
				.Where(error => error.Severity >= validationContext.Severity)
				.ToList();

			if (validationContext.Errors.Any())
			{
				context.Result = new ValidationResult(formatter, errors);
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
			var provider = context.HttpContext.RequestServices;

			var formatter = provider.GetRequiredService<IValidationOutputFormatter>();
			var validationContext = provider.GetRequiredService<IValidationContext>();

			var errors = validationContext.Errors
				.Where(error => error.Severity >= validationContext.Severity)
				.ToList();

			if (validationContext.Errors.Any())
			{
				context.Result = new ValidationResult(formatter, errors);
			}
		}
	}
}