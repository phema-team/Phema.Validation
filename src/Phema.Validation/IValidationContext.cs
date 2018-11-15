using System.Collections.Generic;

namespace Phema.Validation
{
	public interface IValidationContext
	{
		IReadOnlyCollection<IValidationError> Errors { get; }
		IValidationCondition<TValue> Validate<TValue>(ValidationKey key, TValue value);
	}
	
	public class ValidationContext : IValidationContext
	{
		private readonly List<IValidationError> errors;
		
		public ValidationContext()
		{
			errors = new List<IValidationError>();
		}

		public IReadOnlyCollection<IValidationError> Errors => errors;
		
		public IValidationCondition<TValue> Validate<TValue>(ValidationKey key, TValue value)
		{
			return new ValidationCondition<TValue>(value, key, errors);
		}
	}
}