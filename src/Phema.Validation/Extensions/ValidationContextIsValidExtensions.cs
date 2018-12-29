namespace Phema.Validation
{
	public static class ValidationContextIsValidExtensions
	{
		public static bool IsValid(this IValidationContext validationContext)
		{
			return validationContext.IsValid(null);
		}
		
		public static bool IsValid(this IValidationContext validationContext, ValidationKey key)
		{
			return validationContext.IsValid((IValidationKey)key);
		}
	}
}