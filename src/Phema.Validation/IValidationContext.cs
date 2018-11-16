using System.Collections.Generic;

namespace Phema.Validation
{
	public interface IValidationContext
	{
		ValidationSeverity Severity { get; set; }
		IReadOnlyCollection<IValidationError> Errors { get; }
		IValidationCondition<TValue> Validate<TValue>(ValidationKey key, TValue value);
	}
	
	public class ValidationContext : IValidationContext
	{
		private readonly List<IValidationError> errors;
		
		public ValidationContext(ValidationSeverity severity = ValidationSeverity.Error)
		{
			errors = new List<IValidationError>();
			Severity = severity;
		}

		public ValidationSeverity Severity 
		{ 
			get; 
			
			// Хочу ли я меть возможность менять Severity уже созданного контекста
			// Могут ли я менять ее несколько раз?
			// Насколько это багоемко и какие я готов предложить альтернативы?
			set;
		}
		
		public IReadOnlyCollection<IValidationError> Errors => errors;
		
		public IValidationCondition<TValue> Validate<TValue>(ValidationKey key, TValue value)
		{
			return new ValidationCondition<TValue>(value, key, errors);
		}
	}
}