namespace Phema.Validation
{
	public static class ValidationPredicateThrowExtensions
	{
		public static void ThrowMessage<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage,
			ValidationSeverity severity)
		{
			var validationDetail = condition.AddDetail(validationMessage, severity);

			if (validationDetail != null)
			{
				throw new ValidationConditionException(validationDetail);
			}
		}

		public static void ThrowTrace<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			condition.ThrowMessage(validationMessage, ValidationSeverity.Trace);
		}

		public static void ThrowDebug<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			condition.ThrowMessage(validationMessage, ValidationSeverity.Debug);
		}

		public static void ThrowInformation<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			condition.ThrowMessage(validationMessage, ValidationSeverity.Information);
		}

		public static void ThrowWarning<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			condition.ThrowMessage(validationMessage, ValidationSeverity.Warning);
		}

		public static void ThrowError<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			condition.ThrowMessage(validationMessage, ValidationSeverity.Error);
		}

		public static void ThrowFatal<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			condition.ThrowMessage(validationMessage, ValidationSeverity.Fatal);
		}
	}
}