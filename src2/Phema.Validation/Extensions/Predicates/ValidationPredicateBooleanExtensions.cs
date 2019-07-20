namespace Phema.Validation.Conditions
{
	public static class ValidationPredicateBooleanExtensions
	{
		public static IValidationPredicate<bool> IsTrue(
			this IValidationPredicate<bool> predicate)
		{
			return predicate.Is(value => value);
		}

		public static IValidationPredicate<bool> IsFalse(
			this IValidationPredicate<bool> predicate)
		{
			return predicate.Is(value => !value);
		}
	}
}