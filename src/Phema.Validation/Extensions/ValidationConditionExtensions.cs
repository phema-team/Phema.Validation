using System;

namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		/// <summary>
		/// Basic condition check without concrete value usage. Checks if predicate is valid
		/// </summary>
		public static TValidationCondition Is<TValidationCondition>(
			this TValidationCondition condition,
			Func<bool> predicate)
			where TValidationCondition : IValidationCondition
		{
			// Null or true
			if (condition.IsValid != false)
			{
				condition.IsValid = !predicate();
			}

			return condition;
		}

		/// <summary>
		/// Basic condition negative check without concrete value usage. Checks if predicate is not valid
		/// </summary>
		public static TValidationCondition IsNot<TValidationCondition>(
			this TValidationCondition condition,
			Func<bool> predicate)
			where TValidationCondition : IValidationCondition
		{
			return condition.Is(() => !predicate());
		}

		/// <summary>
		/// Checks if value is valid
		/// </summary>
		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> condition,
			Func<TValue, bool> predicate)
		{
			return condition.Is(() => predicate(condition.Value));
		}

		/// <summary>
		/// Checks if value is not valid
		/// </summary>
		public static IValidationCondition<TValue> IsNot<TValue>(
			this IValidationCondition<TValue> condition,
			Func<TValue, bool> predicate)
		{
			return condition.IsNot(() => predicate(condition.Value));
		}
	
		/// <summary>
		/// Adds <see cref="IValidationDetail"/> to <see cref="IValidationContext"/> with specified <see cref="ValidationSeverity"/>
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static IValidationDetail? AddDetail(
			this IValidationCondition condition,
			string validationMessage,
			ValidationSeverity validationSeverity)
		{
			// Not null or false
			// Null by default, so validationContext.When(...).AddDetail(...) works without additional conditions
			if (condition.IsValid == true)
			{
				return null;
			}

			var validationContext = condition.ValidationContext;

			var validationDetail = new ValidationDetail(condition.ValidationKey, validationMessage, validationSeverity);

			validationContext.ValidationDetails.Add(validationDetail);

			if (validationDetail.ValidationSeverity > validationContext.ValidationSeverity)
			{
				throw new ValidationConditionException(validationDetail);
			}

			return validationDetail;
		}

		/// <summary>
		/// Adds <see cref="IValidationDetail"/> to <see cref="IValidationContext"/> with trace severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static IValidationDetail? AddTrace(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Trace);
		}

		/// <summary>
		/// Adds <see cref="IValidationDetail"/> to <see cref="IValidationContext"/> with debug severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static IValidationDetail? AddDebug(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Debug);
		}

		/// <summary>
		/// Adds <see cref="IValidationDetail"/> to <see cref="IValidationContext"/> with information severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static IValidationDetail? AddInformation(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Information);
		}

		/// <summary>
		/// Adds <see cref="IValidationDetail"/> to <see cref="IValidationContext"/> with warning severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static IValidationDetail? AddWarning(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Warning);
		}

		/// <summary>
		/// Adds <see cref="IValidationDetail"/> to <see cref="IValidationContext"/> with error severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static IValidationDetail? AddError(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Error);
		}

		/// <summary>
		/// Adds <see cref="IValidationDetail"/> to <see cref="IValidationContext"/> with fatal severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static IValidationDetail? AddFatal(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Fatal);
		}
	}
}