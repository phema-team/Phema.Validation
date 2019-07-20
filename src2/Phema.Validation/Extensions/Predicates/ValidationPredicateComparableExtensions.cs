using System;

namespace Phema.Validation.Conditions
{
	public static class ValidationPredicateComparableExtensions
	{
		public static IValidationPredicate<TValue> IsGreater<TValue>(
			this IValidationPredicate<TValue> predicate,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return predicate.Is(value => value.CompareTo(comparable) > 0);
		}

		public static IValidationPredicate<TValue> IsLess<TValue>(
			this IValidationPredicate<TValue> predicate,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return predicate.Is(value => value.CompareTo(comparable) < 0);
		}

		public static IValidationPredicate<TValue> IsInRange<TValue>(
			this IValidationPredicate<TValue> predicate,
			TValue min,
			TValue max)
			where TValue : IComparable<TValue>
		{
			return predicate.Is(value => value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0);
		}
	}
}