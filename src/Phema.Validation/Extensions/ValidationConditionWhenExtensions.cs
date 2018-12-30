namespace Phema.Validation
{
	public static class ValidationConditionWhenExtensions
	{
		public static IValidationCondition When(this IValidationContext validationContext)
		{
			return validationContext.When(string.Empty);
		}

		public static IValidationCondition When(this IValidationContext validationContext, ValidationKey key)
		{
			return validationContext.When<object>(key, null);
		}

		public static IValidationCondition<TValue> When<TValue>(this IValidationContext validationContext,
			ValidationKey key, TValue value)
		{
			return validationContext.When(key, value);
		}
	}
}