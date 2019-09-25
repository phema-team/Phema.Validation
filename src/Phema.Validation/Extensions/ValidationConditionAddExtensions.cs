namespace Phema.Validation
{
	public static class ValidationConditionAddExtensions
	{
		/// <summary>
		///   Adds <see cref="ValidationDetail" /> to <see cref="IValidationContext" /> with specified
		///   <see cref="ValidationSeverity" />
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail? AddValidationDetail(
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
		///   Adds <see cref="ValidationDetail" /> to <see cref="IValidationContext" /> with warning severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail? AddValidationWarning(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddValidationDetail(validationMessage, ValidationSeverity.Warning);
		}

		/// <summary>
		///   Adds <see cref="ValidationDetail" /> to <see cref="IValidationContext" /> with error severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail? AddValidationError(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddValidationDetail(validationMessage, ValidationSeverity.Error);
		}

		/// <summary>
		///   Adds <see cref="ValidationDetail" /> to <see cref="IValidationContext" /> with fatal severity
		/// </summary>
		/// <exception cref="ValidationConditionException">
		///   Throws when ValidationDetail.ValidationSeverity greater ValidationContext.ValidationSeverity.
		///   Example: Add fatal detail, when validation context severity is error
		/// </exception>
		public static ValidationDetail? AddValidationFatal(
			this IValidationCondition condition,
			string validationMessage)
		{
			return condition.AddValidationDetail(validationMessage, ValidationSeverity.Fatal);
		}
	}
}