using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public sealed class ValidationContextException : ValidationException
	{
		public ValidationContextException(IReadOnlyCollection<IValidationError> errors, ValidationSeverity severity)
		{
			Errors = errors ?? throw new ArgumentNullException(nameof(errors));
			Severity = severity;
		}

		public IReadOnlyCollection<IValidationError> Errors { get; }

		public ValidationSeverity Severity { get; }
	}
}