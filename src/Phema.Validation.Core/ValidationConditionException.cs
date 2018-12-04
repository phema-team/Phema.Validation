using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public sealed class ValidationConditionException : ValidationException
	{
		public ValidationConditionException(IValidationError error)
		{
			Error = error;
		}

		public IValidationError Error { get; }
	}
}