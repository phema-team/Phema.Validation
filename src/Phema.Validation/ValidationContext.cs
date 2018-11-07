using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public interface IValidationContext
	{
		IReadOnlyCollection<IValidationError> Errors { get; }

		IValidationCondition When(string key);
	}

	public class ValidationContext : IValidationContext
	{
		private readonly List<ValidationError> errors;

		public ValidationContext()
		{
			errors = new List<ValidationError>();
		}

		public IReadOnlyCollection<IValidationError> Errors => errors;

		public IValidationCondition When(string key)
		{
			return new ValidationCondition(key, errors);
		}
	}
}