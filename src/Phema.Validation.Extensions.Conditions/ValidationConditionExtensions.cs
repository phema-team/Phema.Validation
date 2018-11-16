namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition<TValue> WhenNot<TValue>(
			this IValidationCondition<TValue> builder,
			Condition<TValue> condition)
		{
			return builder.When(value => !condition(value));
		}
		
		public static IValidationCondition<TValue> WhenNull<TValue>(
			this IValidationCondition<TValue> builder)
		{
			return builder.When(value => value == null);
		}

		public static IValidationCondition<TValue> WhenNotNull<TValue>(
			this IValidationCondition<TValue> builder)
		{
			return builder.When(value => value != null);
		}

		public static IValidationCondition<TValue> WhenEqual<TValue>(
			this IValidationCondition<TValue> builder,
			TValue expect)
		{
			return builder.When(value => value?.Equals(expect) ?? expect?.Equals(null) ?? true);
		}

		public static IValidationCondition<TValue> WhenNotEqual<TValue>(
			this IValidationCondition<TValue> builder,
			TValue expect)
		{
			return builder.When(value => !(value?.Equals(expect) ?? expect?.Equals(null) ?? true));
		}
	}
}