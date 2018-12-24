namespace Phema.Validation
{
	public static class ValidationContextWhenExtensions
	{
		public static IValidationCondition Validate(this IValidationContext validationContext)
		{
			return validationContext.Validate(new ValidationKey(string.Empty), (object)null);
		}
		
		public static IValidationCondition<TValue> Validate<TValue>(this IValidationContext validationContext, ValidationKey key, TValue value)
		{
			return validationContext.Validate(key, value);
		}
	}
}