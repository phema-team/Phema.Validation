using System;

namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> condition,
			Func<bool> selector)
		{
			return condition.Is(value => selector());
		}
	}
}