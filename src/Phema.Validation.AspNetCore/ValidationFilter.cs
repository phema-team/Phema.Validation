using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public class ValidationFilter : IActionFilter
	{
		public void OnActionExecuting(ActionExecutingContext context)
		{
			var provider = context.HttpContext.RequestServices;
			var options = provider.GetRequiredService<IOptions<ValidationOptions>>().Value;

			var validationContext = provider.GetRequiredService<IValidationContext>();
			
			foreach (var (_, value) in context.ActionArguments)
			{
				if (options.Validations.TryGetValue(value.GetType(), out var factory))
				{
					var validation = factory(provider);
					
					validation.ValidateCore(validationContext, value);
				}
			}

			if (!validationContext.IsValid())
			{
				context.Result = new ValidationResult(validationContext.Errors);
			}
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
		}
	}
}