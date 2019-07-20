namespace Phema.Validation
{
	public static class ValidationPredicateAddExtensions
	{
		public static IValidationDetail AddDetail<TValue>(
			this IValidationCondition<TValue> condition,
			string message,
			ValidationSeverity severity)
		{
			if (condition.IsValid == true)
			{
				return null;
			}

			var validationMessage = new ValidationDetail(condition.ValidationKey, message, severity);

			condition.ValidationContext.ValidationDetails.Add(validationMessage);
			return validationMessage;
		}

		public static IValidationDetail AddTrace<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddDetail(message, ValidationSeverity.Trace);
		}

		public static IValidationDetail AddDebug<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddDetail(message, ValidationSeverity.Debug);
		}

		public static IValidationDetail AddInformation<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddDetail(message, ValidationSeverity.Information);
		}

		public static IValidationDetail AddWarning<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddDetail(message, ValidationSeverity.Warning);
		}

		public static IValidationDetail AddError<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddDetail(message, ValidationSeverity.Error);
		}

		public static IValidationDetail AddFatal<TValue>(this IValidationCondition<TValue> condition, string message)
		{
			return condition.AddDetail(message, ValidationSeverity.Fatal);
		}
	}
}