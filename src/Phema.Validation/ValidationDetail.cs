using System;

namespace Phema.Validation
{
	/// <summary>
	/// Stores failed validation result details. Inherit for custom validation properties
	/// </summary>
	public class ValidationDetail
	{
		public ValidationDetail(string validationKey, string validationMessage, ValidationSeverity validationSeverity)
		{
			ValidationKey = validationKey ?? throw new ArgumentNullException(nameof(validationKey));
			ValidationMessage = validationMessage ?? throw new ArgumentNullException(nameof(validationMessage));
			ValidationSeverity = validationSeverity;
		}

		/// <summary>
		/// Specified validation key with validation context prefix
		/// </summary>
		public string ValidationKey { get; }

		/// <summary>
		/// Validation detail message
		/// </summary>
		public string ValidationMessage { get; }

		/// <summary>
		/// Validation detail severity. If greater than ValidationContext.ValidationSeverity will throw <see cref="ValidationConditionException"/>
		/// </summary>
		public ValidationSeverity ValidationSeverity { get; }
	}
}