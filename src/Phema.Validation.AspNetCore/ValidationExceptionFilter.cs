using Microsoft.AspNetCore.Mvc.Filters;

namespace Phema.Validation
{
	internal sealed class ValidationExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			switch (context.Exception)
			{
				case ValidationContextException exception:
					context.Result = new ValidationResult(exception.Errors);
					break;
				
				case ValidationConditionException exception:
					context.Result = new ValidationResult(exception.Error);
					break;
			}
		}
	}
}