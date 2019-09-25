using System;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionComparableExtensions
	{
		/// <summary>
		///   Checks value is greater
		/// </summary>
		public static IValidationCondition<TValue> IsGreater<TValue>(
			this IValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) > 0);
		}

		/// <summary>
		///   Checks value is greater or equal
		/// </summary>
		public static IValidationCondition<TValue> IsGreaterOrEqual<TValue>(
			this IValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) >= 0);
		}

		/// <summary>
		///   Checks value is less
		/// </summary>
		public static IValidationCondition<TValue> IsLess<TValue>(
			this IValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) < 0);
		}

		/// <summary>
		///   Checks value is less or equal
		/// </summary>
		public static IValidationCondition<TValue> IsLessOrEqual<TValue>(
			this IValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) <= 0);
		}

		/// <summary>
		///   Checks value is in range (min: inclusive, max: exclusive)
		/// </summary>
		public static IValidationCondition<TValue> IsInRange<TValue>(
			this IValidationCondition<TValue> condition,
			TValue min,
			TValue max)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(min) >= 0 && value.CompareTo(max) < 0);
		}

		/// <summary>
		///   Checks value is not in range (min: inclusive, max: exclusive)
		/// </summary>
		public static IValidationCondition<TValue> IsNotInRange<TValue>(
			this IValidationCondition<TValue> condition,
			TValue min,
			TValue max)
			where TValue : IComparable<TValue>
		{
			return condition.IsNot(value => value.CompareTo(min) >= 0 && value.CompareTo(max) < 0);
		}
	}
}