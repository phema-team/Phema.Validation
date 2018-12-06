using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public sealed class ValidationContextException : ValidationException
	{
		public ValidationContextException(IReadOnlyCollection<IValidationError> errors)
		{
			Errors = errors;
		}
		
		public IReadOnlyCollection<IValidationError> Errors { get; }
	}
}