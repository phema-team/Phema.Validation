using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	public static class ValidationConditionEnumerableExtensionsValidationConditionEnumerableExtensions
	{
		public static IValidationCondition<IEnumerable<TElement>> IsAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> builder)
		{
			return builder.Is(value => value?.Any() ?? false);
		}
		
		public static IValidationCondition<IEnumerable<TElement>> IsAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> builder,
			Func<TElement, bool> predicate)
		{
			return builder.Is(value => value?.Any(predicate) ?? false);
		}
		
		public static IValidationCondition<IEnumerable<TElement>> IsAll<TElement>(
			this IValidationCondition<IEnumerable<TElement>> builder,
			Func<TElement, bool> predicate)
		{
			return builder.Is(value => value?.All(predicate) ?? false);
		}
	}
}