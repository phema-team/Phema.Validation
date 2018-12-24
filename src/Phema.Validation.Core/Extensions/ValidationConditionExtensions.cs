using System;

namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition<TElement> Is<TElement>(
			this IValidationCondition<TElement> builder,
			Func<TElement, bool> condition)
		{
			return builder.Condition((value, _) => condition(value));
		}
		
		public static IValidationCondition<TElement> When<TElement>(
			this IValidationCondition<TElement> builder,
			Func<TElement, bool> condition)
		{
			return builder.Condition((value, added) => !added && condition(value));
		}
	}
}