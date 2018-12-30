using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Phema.Validation
{
	internal sealed class ValidationExceptionFilter : IExceptionFilter
	{
		private readonly IValidationOutputFormatter formatter;

		public ValidationExceptionFilter(IValidationOutputFormatter formatter)
		{
			if (formatter == null)
				throw new ArgumentNullException(nameof(formatter));

			this.formatter = formatter;
		}

		public void OnException(ExceptionContext context)
		{
			switch (context.Exception)
			{
				case ValidationContextException exception:
					var errors = exception.Errors
						.Where(error => error.Severity >= exception.Severity)
						.ToList();

					context.Result = new ValidationResult(formatter, errors);
					break;

				case ValidationConditionException exception:
					context.Result = new ValidationResult(formatter, new[]
					{
						exception.Error
					});
					break;
			}
		}
	}
}