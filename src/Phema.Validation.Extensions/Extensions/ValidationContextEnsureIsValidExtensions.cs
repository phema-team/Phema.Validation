using System.Linq;

namespace Phema.Validation
{
	public static class ValidationContextEnsureIsValidExtensions
	{
		public static void EnsureIsValid(this IValidationContext validationContext, IValidationKey validationKey)
		{
			if (!validationContext.IsValid(validationKey))
			{
				var errors = validationContext.Errors
					.Where(error => error.Severity >= validationContext.Severity)
					.ToArray();

				throw new ValidationContextException(errors, validationContext.Severity);
			}
		}

		public static void EnsureIsValid(this IValidationContext validationContext)
		{
			validationContext.EnsureIsValid(null);
		}

		public static void EnsureIsValid(this IValidationContext validationContext, ValidationKey key)
		{
			validationContext.EnsureIsValid((IValidationKey)key);
		}
	}
}