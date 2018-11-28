using System;

namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> validationCondition,
			Func<bool> condition)
		{
			return validationCondition.Is(value => condition());
		}
	}
}