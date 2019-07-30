namespace Phema.Validation
{
	public static class ValidationConditionAddExtensions
	{
		/// <summary>
		/// Adds <see cref="ValidationDetail"/> to <see cref="IValidationContext"/> with specified <see cref="ValidationSeverity"/>
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail AddDetail(
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
		/// Adds <see cref="ValidationDetail"/> to <see cref="IValidationContext"/> with trace severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail AddTrace(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Trace);
		}

		/// <summary>
		/// Adds <see cref="ValidationDetail"/> to <see cref="IValidationContext"/> with debug severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail AddDebug(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Debug);
		}

		/// <summary>
		/// Adds <see cref="ValidationDetail"/> to <see cref="IValidationContext"/> with information severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail AddInformation(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Information);
		}

		/// <summary>
		/// Adds <see cref="ValidationDetail"/> to <see cref="IValidationContext"/> with warning severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail AddWarning(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Warning);
		}

		/// <summary>
		/// Adds <see cref="ValidationDetail"/> to <see cref="IValidationContext"/> with error severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail AddError(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Error);
		}

		/// <summary>
		/// Adds <see cref="ValidationDetail"/> to <see cref="IValidationContext"/> with fatal severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail AddFatal(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddDetail(validationMessage, ValidationSeverity.Fatal);
		}
	}
}