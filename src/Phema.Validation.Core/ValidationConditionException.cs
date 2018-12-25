using System;
using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public sealed class ValidationConditionException : ValidationException
	{
		public ValidationConditionException(IValidationError error)
		{
			if (error == null)
				throw new ArgumentNullException(nameof(error));
			
			Error = error;
		}

		public IValidationError Error { get; }
	}
}