using System;

namespace Phema.Validation
{
	public sealed class ValidationConditionException : Exception
	{
		public ValidationConditionException(IValidationDetail validationDetail)
		{
			ValidationDetail = validationDetail;
		}

		public IValidationDetail ValidationDetail { get; }
	}
}