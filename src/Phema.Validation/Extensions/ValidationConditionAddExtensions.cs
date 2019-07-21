namespace Phema.Validation
{
	public static class ValidationConditionAddExtensions
	{
		/// <summary>
		/// Adds <see cref="IValidationDetail"/> to <see cref="IValidationContext"/> with specified <see cref="ValidationSeverity"/>
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static IValidationDetail AddDetail<TValue>(
			this IValidationCondition<TValue> condition,
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
		public static IValidationDetail AddTrace<TValue>(
			this IValidationCondition<TValue> condition,
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
		public static IValidationDetail AddDebug<TValue>(
			this IValidationCondition<TValue> condition,
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
		public static IValidationDetail AddInformation<TValue>(
			this IValidationCondition<TValue> condition,
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
		public static IValidationDetail AddWarning<TValue>(
			this IValidationCondition<TValue> condition,
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
		public static IValidationDetail AddError<TValue>(
			this IValidationCondition<TValue> condition,
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
		public static IValidationDetail AddFatal<TValue>(
			this IValidationCondition<TValue> condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Fatal);
		}
	}
}