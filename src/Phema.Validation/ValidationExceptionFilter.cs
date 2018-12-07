using System.Linq;
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

					var errors = exception.Errors
						.Where(error => error.Severity >= exception.Severity);
					
					context.Result = new ValidationResult(errors);
					break;
				
				case ValidationConditionException exception:
					context.Result = new ValidationResult(exception.Error);
					break;
			}
		}
	}
}