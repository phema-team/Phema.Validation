namespace Phema.Validation
{
	public static class ValidationPredicateAddExtensions
	{
		public static IValidationMessage AddMessage<TValue>(
			this IValidationPredicate<TValue> predicate,
			string message,
			ValidationSeverity severity)
		{
			if (predicate.IsValid == true)
			{
				return null;
			}

			var validationMessage = new ValidationMessage(predicate.ValidationKey, message, severity);

			predicate.ValidationContext.ValidationMessages.Add(validationMessage);
			return validationMessage;
		}

		public static IValidationMessage AddTrace<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			return predicate.AddMessage(message, ValidationSeverity.Trace);
		}

		public static IValidationMessage AddDebug<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			return predicate.AddMessage(message, ValidationSeverity.Debug);
		}

		public static IValidationMessage AddInformation<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			return predicate.AddMessage(message, ValidationSeverity.Information);
		}

		public static IValidationMessage AddWarning<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			return predicate.AddMessage(message, ValidationSeverity.Warning);
		}

		public static IValidationMessage AddError<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			return predicate.AddMessage(message, ValidationSeverity.Error);
		}

		public static IValidationMessage AddFatal<TValue>(this IValidationPredicate<TValue> predicate, string message)
		{
			return predicate.AddMessage(message, ValidationSeverity.Fatal);
		}
	}
}