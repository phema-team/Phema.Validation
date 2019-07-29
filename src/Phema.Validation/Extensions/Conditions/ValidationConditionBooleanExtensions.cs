namespace Phema.Validation.Conditions
{
	public static class ValidationBooleanExtensions
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