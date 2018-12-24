using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	public static class ValidationConditionWhenEnumerableExtensions
	{
		public static IValidationCondition<IEnumerable<TElement>> WhenAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> builder)
		{
			return builder.When(value => value?.Any() ?? false);
		}
		
		public static IValidationCondition<IEnumerable<TElement>> WhenAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> builder,
			Func<TElement, bool> predicate)
		{
			return builder.When(value => value?.Any(predicate) ?? false);
		}
		
		public static IValidationCondition<IEnumerable<TElement>> WhenAll<TElement>(
			this IValidationCondition<IEnumerable<TElement>> builder,
			Func<TElement, bool> predicate)
		{
			return builder.When(value => value?.All(predicate) ?? false);
		}
	}
}