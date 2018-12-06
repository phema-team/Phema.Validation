using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IEnumerable<IValidationError> SevereErrors(this IValidationContext validationContext)
		{
			return validationContext.Errors.Where(error => error.Severity >= validationContext.Severity);
		}
	}
}