namespace Phema.Validation
{
	public static class ValidationConditionIsBoolExtensions
	{
		public static IValidationCondition<bool> IsTrue(
			this IValidationCondition<bool> builder)
		{
			return builder.Is(value => value);
		}

		public static IValidationCondition<bool> IsFalse(
			this IValidationCondition<bool> builder)
		{
			return builder.Is(value => !value);
		}
	}
}