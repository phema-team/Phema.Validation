using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public class ValidationContextException : Exception
	{
		internal ValidationContextException(IReadOnlyCollection<IValidationError> errors)
		{
			Errors = errors;
		}

		public IReadOnlyCollection<IValidationError> Errors { get; }
	}
}