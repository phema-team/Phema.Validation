namespace Phema.Validation.Conditions
{
	public static class ValidationBooleanExtensions
	{
		/// <summary>
		///   Checks value is true
		/// </summary>
		public static IValidationCondition<bool> IsTrue(
			this IValidationCondition<bool> condition)
		{
			return condition.Is(value => value);
		}

		/// <summary>
		///   Checks value is false
		/// </summary>
		public static IValidationCondition<bool> IsFalse(
			this IValidationCondition<bool> condition)
		{
			return condition.IsNot(value => value);
		}
	}
}