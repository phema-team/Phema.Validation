using System;

namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationError Add(
			this IValidationCondition condition,
			Func<ValidationMessage> selector,
			ValidationSeverity severity)
		{
			return condition.Add(selector, null, severity);
		}
	}
}