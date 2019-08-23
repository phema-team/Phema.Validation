using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public sealed class ValidationConditionException : ValidationException
	{
		public ValidationConditionException(ValidationDetail validationDetail)
		{
			ValidationDetail = validationDetail;
		}

		public ValidationDetail ValidationDetail { get; }
	}
}