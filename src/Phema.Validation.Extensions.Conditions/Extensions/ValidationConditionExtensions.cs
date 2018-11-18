namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition<TValue> IsNot<TValue>(
			this IValidationCondition<TValue> builder,
			Condition<TValue> condition)
		{
			return builder.Is(value => !condition(value));
		}
		
		public static IValidationCondition<TValue> IsNull<TValue>(
			this IValidationCondition<TValue> builder)
		{
			return builder.Is(value => value == null);
		}

		public static IValidationCondition<TValue> IsNotNull<TValue>(
			this IValidationCondition<TValue> builder)
		{
			return builder.Is(value => value != null);
		}

		public static IValidationCondition<TValue> IsEqual<TValue>(
			this IValidationCondition<TValue> builder,
			TValue expect)
		{
			return builder.Is(value => value?.Equals(expect) ?? expect?.Equals(null) ?? true);
		}

		public static IValidationCondition<TValue> IsNotEqual<TValue>(
			this IValidationCondition<TValue> builder,
			TValue expect)
		{
			return builder.Is(value => !(value?.Equals(expect) ?? expect?.Equals(null) ?? true));
		}
	}
}