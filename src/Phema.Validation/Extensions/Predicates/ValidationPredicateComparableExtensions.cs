using System;

namespace Phema.Validation.Conditions
{
	public static class ValidationPredicateComparableExtensions
	{
		public static IValidationCondition<TValue> IsGreater<TValue>(
			this IValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) > 0);
		}

		public static IValidationCondition<TValue> IsGreaterOrEqual<TValue>(
			this IValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) >= 0);
		}

		public static IValidationCondition<TValue> IsLess<TValue>(
			this IValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) < 0);
		}
		
		public static IValidationCondition<TValue> IsLessOrEqual<TValue>(
			this IValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) <= 0);
		}

		public static IValidationCondition<TValue> IsInRange<TValue>(
			this IValidationCondition<TValue> condition,
			TValue min,
			TValue max)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0);
		}
	}
}