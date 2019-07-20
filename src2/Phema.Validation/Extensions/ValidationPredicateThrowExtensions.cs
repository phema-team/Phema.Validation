namespace Phema.Validation
{
	public static class ValidationPredicateThrowExtensions
	{
		public static void ThrowMessage<TValue>(
			this IValidationPredicate<TValue> predicate,
			string message,
			ValidationSeverity severity)
		{
			var validationMessage = predicate.AddMessage(message, severity);

			if (validationMessage != null)
			{
				throw new ValidationPredicateException(validationMessage);
			}
		}

		public static void ThrowTrace<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			predicate.ThrowMessage(message, ValidationSeverity.Trace);
		}

		public static void ThrowDebug<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			predicate.ThrowMessage(message, ValidationSeverity.Debug);
		}

		public static void ThrowInformation<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			predicate.ThrowMessage(message, ValidationSeverity.Information);
		}

		public static void ThrowWarning<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			predicate.ThrowMessage(message, ValidationSeverity.Warning);
		}

		public static void ThrowError<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			predicate.ThrowMessage(message, ValidationSeverity.Error);
		}

		public static void ThrowFatal<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			predicate.ThrowMessage(message, ValidationSeverity.Fatal);
		}
	}
}