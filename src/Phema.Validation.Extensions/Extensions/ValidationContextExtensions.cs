using System.Linq;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static bool IsValid(this IValidationContext validationContext, IValidationKey validationKey)
		{
			return !validationContext
				.Errors
				.Where(error => error.Severity >= validationContext.Severity)
				.Any(error => validationKey == null || error.Key == validationKey.Key);
		}
		
		public static void EnsureIsValid(this IValidationContext validationContext, IValidationKey validationKey)
		{
			if (!validationContext.IsValid(validationKey))
			{
				throw new ValidationContextException(validationContext.Errors, validationContext.Severity);
			}
		}
	}
}