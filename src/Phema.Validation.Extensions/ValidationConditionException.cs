using System;
using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public class ValidationConditionException : ValidationException
	{
		internal ValidationConditionException(IValidationError error)
		{
			Error = error;
		}

		public IValidationError Error { get; }
	}
}