using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionEnumerableExtensions
	{
		public static IValidationCondition<IEnumerable<TElement>> IsAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> condition)
		{
			return condition.Is(value => value?.Any() ?? false);
		}

		public static IValidationCondition<IEnumerable<TElement>> IsAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> condition,
			Func<TElement, bool> predicate)
		{
			return condition.Is(value => value?.Any(predicate) ?? false);
		}

		public static IValidationCondition<IEnumerable<TElement>> IsAll<TElement>(
			this IValidationCondition<IEnumerable<TElement>> condition,
			Func<TElement, bool> predicate)
		{
			return condition.Is(value => value?.All(predicate) ?? false);
		}
	}
}