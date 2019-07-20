using System.Collections.Generic;

namespace Phema.Validation.Conditions
{
	public static class ValidationPredicateCollectonExtensions
	{
		public static IValidationPredicate<ICollection<TElement>> IsEmpty<TElement>(
			this IValidationPredicate<ICollection<TElement>> predicate)
		{
			return predicate.Is(value => value == null || value.Count == 0);
		}

		public static IValidationPredicate<ICollection<TElement>> IsNotEmpty<TElement>(
			this IValidationPredicate<ICollection<TElement>> predicate)
		{
			return predicate.Is(value => value != null && value.Count != 0);
		}

		public static IValidationPredicate<ICollection<TElement>> HasCount<TElement>(
			this IValidationPredicate<ICollection<TElement>> predicate,
			int count)
		{
			return predicate.Is(value => value != null && value.Count == count);
		}

		public static IValidationPredicate<ICollection<TElement>> NotHasCount<TElement>(
			this IValidationPredicate<ICollection<TElement>> predicate,
			int count)
		{
			return predicate.Is(value => value != null && value.Count != count);
		}

		public static IValidationPredicate<ICollection<TElement>> IsContains<TElement>(
			this IValidationPredicate<ICollection<TElement>> predicate,
			TElement element)
		{
			return predicate.Is(value => value?.Contains(element) ?? false);
		}

		public static IValidationPredicate<ICollection<TElement>> IsNotContains<TElement>(
			this IValidationPredicate<ICollection<TElement>> predicate,
			TElement element)
		{
			return predicate.Is(value => !(value?.Contains(element) ?? false));
		}
	}
}