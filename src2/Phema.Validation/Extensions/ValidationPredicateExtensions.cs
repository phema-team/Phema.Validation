using System;

namespace Phema.Validation
{
	public static class ValidationPredicateExtensions
	{
		public static IValidationPredicate<TValue> Is<TValue>(
			this IValidationPredicate<TValue> predicate,
			Func<TValue, bool> condition)
		{
			if (predicate.IsValid != false)
			{
				predicate.IsValid = !condition(predicate.Value);
			}

			return predicate;
		}

		public static IValidationPredicate<TValue> Is<TValue>(
			this IValidationPredicate<TValue> predicate,
			Func<bool> condition)
		{
			return predicate.Is(value => condition());
		}

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