using System;

namespace Phema.Validation
{
	/// <summary>
	/// Stores failed validation result details. Inherit for custom validation properties
	/// </summary>
	public interface IValidationDetail
	{
		/// <summary>
		/// Specified validation key with validation context path
		/// </summary>
		string ValidationKey { get; }

		/// <summary>
		/// Validation detail message
		/// </summary>
		string ValidationMessage { get; }

		/// <summary>
		/// Validation detail severity. If greater than ValidationContext.ValidationSeverity will throw <see cref="ValidationConditionException"/>
		/// </summary>
		ValidationSeverity ValidationSeverity { get; }
	}

	// public for external use
	public sealed class ValidationDetail : IValidationDetail
	{
		public ValidationDetail(string validationKey, string validationMessage, ValidationSeverity validationSeverity)
		{
			ValidationKey = validationKey ?? throw new ArgumentNullException(nameof(validationKey));
			ValidationMessage = validationMessage ?? throw new ArgumentNullException(nameof(validationMessage));
			ValidationSeverity = validationSeverity;
		}

		public string ValidationKey { get; }

		public string ValidationMessage { get; }

		public ValidationSeverity ValidationSeverity { get; }
	}
}