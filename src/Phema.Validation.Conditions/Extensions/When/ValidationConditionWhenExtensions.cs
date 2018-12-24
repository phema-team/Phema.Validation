namespace Phema.Validation
{
	public static class ValidationConditionWhenExtensions
	{
		public static IValidationCondition<TValue> WhenNull<TValue>(
			this IValidationCondition<TValue> builder)
		{
			return builder.When((value) => value == null);
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