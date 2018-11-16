using System.Collections.Generic;

namespace Phema.Validation
{
	public interface IValidationContext
	{
		ValidationSeverity Severity { get; }
		IReadOnlyCollection<IValidationError> Errors { get; }
		IValidationCondition<TValue> When<TValue>(ValidationKey key, TValue value);
	}
	
	public class ValidationContext : IValidationContext
	{
		private readonly List<IValidationError> errors;
		
		public ValidationContext()
		{
			errors = new List<IValidationError>();
			Severity = ValidationSeverity.Error;
		}

		public ValidationContext(ValidationSeverity severity)
		{
			errors = new List<IValidationError>();
			Severity = severity;
		}

		public ValidationSeverity Severity { get; }
		
		public IReadOnlyCollection<IValidationError> Errors => errors;
		
		public IValidationCondition<TValue> When<TValue>(ValidationKey key, TValue value)
		{
			return new ValidationCondition<TValue>(value, key, errors);
		}
	}
}