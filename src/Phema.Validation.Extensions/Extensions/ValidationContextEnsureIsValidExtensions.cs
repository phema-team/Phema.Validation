namespace Phema.Validation
{
	public static class ValidationContextEnsureIsValidExtensions
	{
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
		
		public static void EnsureIsValid(this IValidationContext validationContext, ValidationKey key)
		{
			validationContext.EnsureIsValid((IValidationKey)key);
		}
	}
}