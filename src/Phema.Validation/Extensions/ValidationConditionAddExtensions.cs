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
			ValidationSeverity validationSeverity = ValidationSeverity.Error)
		{
			var validationContext = condition.ValidationContext;

			var validationDetail = new ValidationDetail(
				validationContext,
				condition.ValidationKey,
				validationMessage,
				validationSeverity,
				condition.IsValid ?? false);

			// Not null or false
			// Null by default, so validationContext.When(...).AddValidationDetail(...) works without additional conditions
			if (validationDetail.IsValid)
			{
				return validationDetail;
			}

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