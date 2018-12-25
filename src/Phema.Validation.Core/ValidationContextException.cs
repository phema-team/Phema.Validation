using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public sealed class ValidationContextException : ValidationException
	{
		public ValidationContextException(IReadOnlyCollection<IValidationError> errors, ValidationSeverity severity)
		{
			if (errors == null)
				throw new ArgumentNullException(nameof(errors));
			
			Errors = errors;
			Severity = severity;
		}
		
		public IReadOnlyCollection<IValidationError> Errors { get; }

		public ValidationSeverity Severity { get; }
	}
}