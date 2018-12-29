namespace Phema.Validation
{
	public static class ValidationContextWhenExtensions
	{
		public static IValidationCondition Validate(this IValidationContext validationContext)
		{
			return validationContext.Validate((ValidationKey)string.Empty, (object)null);
		}
		
		public static IValidationCondition<object> Validate(this IValidationContext validationContext, ValidationKey key)
		{
			return validationContext.Validate(key, (object)null);
		}
		
		public static IValidationCondition<TValue> Validate<TValue>(this IValidationContext validationContext, ValidationKey key, TValue value)
		{
			return validationContext.Validate(key, value);
		}
	}
}