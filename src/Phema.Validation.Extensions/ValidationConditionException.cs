using System;

namespace Phema.Validation
{
	public class ValidationConditionException : Exception
	{
		internal ValidationConditionException(IValidationError error)
		{
			Error = error;
		}

		public IValidationError Error { get; }
	}
}