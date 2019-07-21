using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public sealed class ValidationConditionException : ValidationException
	{
		public ValidationConditionException(IValidationDetail validationDetail)
		{
			ValidationDetail = validationDetail;
		}

		public IValidationDetail ValidationDetail { get; }
	}
}