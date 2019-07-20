using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation.Conditions
{
	public static class ValidationPredicateEnumerableExtensions
	{
		public static IValidationPredicate<IEnumerable<TElement>> IsAny<TElement>(
			this IValidationPredicate<IEnumerable<TElement>> predicate)
		{
			return predicate.Is(value => value?.Any() ?? false);
		}

		public static IValidationPredicate<IEnumerable<TElement>> IsAny<TElement>(
			this IValidationPredicate<IEnumerable<TElement>> predicate,
			Func<TElement, bool> condition)
		{
			return predicate.Is(value => value?.Any(condition) ?? false);
		}

		public static IValidationPredicate<IEnumerable<TElement>> IsAll<TElement>(
			this IValidationPredicate<IEnumerable<TElement>> predicate,
			Func<TElement, bool> condition)
		{
			return predicate.Is(value => value?.All(condition) ?? false);
		}
	}
}