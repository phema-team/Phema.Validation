namespace Phema.Validation
{
	public static class ValidationPredicateAddExtensions
	{
		public static IValidationMessage AddMessage<TValue>(
			this IValidationCondition<TValue> condition,
			string message,
			ValidationSeverity severity)
		{
			if (condition.IsValid == true)
			{
				return null;
			}

			var validationMessage = new ValidationMessage(condition.ValidationKey, message, severity);

			condition.ValidationContext.ValidationMessages.Add(validationMessage);
			return validationMessage;
		}

		public static IValidationMessage AddTrace<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddMessage(message, ValidationSeverity.Trace);
		}

		public static IValidationMessage AddDebug<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddMessage(message, ValidationSeverity.Debug);
		}

		public static IValidationMessage AddInformation<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddMessage(message, ValidationSeverity.Information);
		}

		public static IValidationMessage AddWarning<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddMessage(message, ValidationSeverity.Warning);
		}

		public static IValidationMessage AddError<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddMessage(message, ValidationSeverity.Error);
		}

		public static IValidationMessage AddFatal<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddMessage(message, ValidationSeverity.Fatal);
		}
	}
}