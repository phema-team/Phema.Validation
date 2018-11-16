using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	public static class ValidationConditionEnumerableExtensions
	{
		public static IValidationCondition<ICollection<TElement>> IsAny<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			Func<TElement, bool> predicate)
		{
			return builder.Is(value => value?.Any(predicate) ?? false);
		}
		
		public static IValidationCondition<ICollection<TElement>> IsAny<TElement>(
			this IValidationCondition<ICollection<TElement>> builder)
		{
			return builder.Is(value => value?.Any() ?? false);
		}
		
		public static IValidationCondition<ICollection<TElement>> IsAll<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			Func<TElement, bool> predicate)
		{
			return builder.Is(value => value?.All(predicate) ?? false);
		}
	}
}