using System;
using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public sealed class ValidationConditionException : ValidationException
	{
		public ValidationConditionException(IValidationError error)
		{
			Error = error ?? throw new ArgumentNullException(nameof(error));
		}

		public IValidationError Error { get; }
	}
}