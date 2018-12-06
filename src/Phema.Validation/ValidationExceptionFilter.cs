using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Phema.Validation
{
	internal sealed class ValidationExceptionFilter : IExceptionFilter
	{
		private readonly IValidationContext validationContext;

		public ValidationExceptionFilter(IValidationContext validationContext)
		{
			this.validationContext = validationContext;
		}
		
		public void OnException(ExceptionContext context)
		{
			switch (context.Exception)
			{
				case ValidationException _:
					context.Result = new ValidationResult(validationContext.SevereErrors());
					break;
			}
		}
	}
}