using System;

namespace Phema.Validation
{
	public static class ValidationPredicateAddExtensions
	{
		public static IValidationDetail AddDetail<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage,
			ValidationSeverity severity)
		{
			if (validationMessage is null)
				throw new ArgumentNullException(nameof(validationMessage));

			// Not null or false
			if (condition.IsValid == true)
			{
				return null;
			}

			var validationDetail = new ValidationDetail(condition.ValidationKey, validationMessage, severity);

			condition.ValidationContext.ValidationDetails.Add(validationDetail);
			return validationDetail;
		}

		public static IValidationDetail AddTrace<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Trace);
		}

		public static IValidationDetail AddDebug<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Debug);
		}

		public static IValidationDetail AddInformation<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Information);
		}

		public static IValidationDetail AddWarning<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Warning);
		}

		public static IValidationDetail AddError<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Error);
		}

		public static IValidationDetail AddFatal<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Fatal);
		}
	}
}