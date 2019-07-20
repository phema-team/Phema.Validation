using System;

namespace Phema.Validation
{
	public sealed class ValidationPredicateException : Exception
	{
		public ValidationPredicateException(IValidationMessage validationMessage)
		{
			ValidationMessage = validationMessage;
		}

		public IValidationMessage ValidationMessage { get; }
	}
}