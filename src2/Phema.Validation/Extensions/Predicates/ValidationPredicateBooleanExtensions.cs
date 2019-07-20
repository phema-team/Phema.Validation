namespace Phema.Validation.Conditions
{
	public static class ValidationPredicateBooleanExtensions
	{
		public static IValidationCondition<bool> IsTrue(
			this IValidationCondition<bool> condition)
		{
			return condition.Is(value => value);
		}

		public static IValidationCondition<bool> IsFalse(
			this IValidationCondition<bool> condition)
		{
			return condition.Is(value => !value);
		}
	}
}