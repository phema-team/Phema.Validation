namespace Phema.Validation
{
	public static class ValidationContextIsValidExtensions
	{
		public static bool IsValid(
			this IValidationContext validationContext, 
			ValidationKey key)
		{
			return validationContext.IsValid((IValidationKey)key);
		}
		
		public static bool IsValid(this IValidationContext validationContext)
		{
			return validationContext.IsValid(null);
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