using System;

namespace Phema.Validation
{
	public sealed class ValidationConditionException : Exception
	{
		public ValidationConditionException(IValidationMessage validationMessage)
		{
			ValidationMessage = validationMessage;
		}

		public IValidationMessage ValidationMessage { get; }
	}
}