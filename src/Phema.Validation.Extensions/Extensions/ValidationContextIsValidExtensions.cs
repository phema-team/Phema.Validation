using System.Linq;

namespace Phema.Validation
{
	public static class ValidationContextIsValidExtensions
	{
		public static bool IsValid(this IValidationContext validationContext, IValidationKey validationKey)
		{
			return !validationContext
				.Errors
				.Where(error => error.Severity >= validationContext.Severity)
				.Any(error => validationKey == null || error.Key == validationKey.Key);
		}
		
		public static bool IsValid(this IValidationContext validationContext)
		{
			return validationContext.IsValid(null);
		}
		
		public static bool IsValid(
			this IValidationContext validationContext, 
			ValidationKey key)
		{
			return validationContext.IsValid((IValidationKey)key);
		}
		
		public static void EnsureIsValid(this IValidationContext validationContext, IValidationKey validationKey)
		{
			if (!validationContext.IsValid(validationKey))
			{
				throw new ValidationContextException(validationContext.Errors, validationContext.Severity);
			}
		}

		public static void EnsureIsValid(this IValidationContext validationContext)
		{
			validationContext.EnsureIsValid(null);
		}
		
		public static void EnsureIsValid(
			this IValidationContext validationContext, 
			ValidationKey key)
		{
			validationContext.EnsureIsValid((IValidationKey)key);
		}
	}
}