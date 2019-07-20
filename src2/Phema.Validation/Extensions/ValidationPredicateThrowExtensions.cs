namespace Phema.Validation
{
	public static class ValidationPredicateThrowExtensions
	{
		public static void ThrowMessage<TValue>(
			this IValidationCondition<TValue> condition,
			string message,
			ValidationSeverity severity)
		{
			var validationMessage = condition.AddMessage(message, severity);

			if (validationMessage != null)
			{
				throw new ValidationPredicateException(validationMessage);
			}
		}

		public static void ThrowTrace<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			condition.ThrowMessage(message, ValidationSeverity.Trace);
		}

		public static void ThrowDebug<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			condition.ThrowMessage(message, ValidationSeverity.Debug);
		}

		public static void ThrowInformation<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			condition.ThrowMessage(message, ValidationSeverity.Information);
		}

		public static void ThrowWarning<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			condition.ThrowMessage(message, ValidationSeverity.Warning);
		}

		public static void ThrowError<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			condition.ThrowMessage(message, ValidationSeverity.Error);
		}

		public static void ThrowFatal<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			condition.ThrowMessage(message, ValidationSeverity.Fatal);
		}
	}
}