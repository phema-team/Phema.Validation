namespace Phema.Validation
{
	public static class ValidationContextWhenExtensions
	{
		public static IValidationCondition When(this IValidationContext validationContext)
		{
			return validationContext.When(new ValidationKey(string.Empty), (object)null);
		}
		
		public static IValidationCondition<TValue> When<TValue>(this IValidationContext validationContext, ValidationKey key, TValue value)
		{
			return validationContext.When(key, value);
		}
	}
}