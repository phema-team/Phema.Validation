using System.Collections.Generic;
using System.Globalization;

namespace Phema.Validation
{
	public interface IValidationContext
	{
		ValidationSeverity Severity { get; }
		IReadOnlyCollection<IValidationError> Errors { get; }
		IValidationCondition<TValue> When<TValue>(ValidationKey key, TValue value);
	}
	
	public sealed class ValidationContext : IValidationContext
	{
		private readonly CultureInfo cultureInfo;
		private readonly List<IValidationError> errors;
		
		public ValidationContext(ValidationSeverity severity = ValidationSeverity.Error, CultureInfo cultureInfo = null)
		{
			Severity = severity;
			errors = new List<IValidationError>();
			this.cultureInfo = cultureInfo ?? CultureInfo.InvariantCulture;
		}

		public ValidationSeverity Severity { get; }
		
		public IReadOnlyCollection<IValidationError> Errors => errors;
		
		public IValidationCondition<TValue> When<TValue>(ValidationKey key, TValue value)
		{
			return new ValidationCondition<TValue>(value, key, errors, cultureInfo);
		}
	}
}