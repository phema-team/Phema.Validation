using System;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionComparableExtensions
	{
		/// <summary>
		///   Checks value is greater
		/// </summary>
		public static ValidationCondition<TValue> IsGreater<TValue>(
			this ValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) > 0);
		}

		/// <summary>
		///   Checks value is greater or equal
		/// </summary>
		public static ValidationCondition<TValue> IsGreaterOrEqual<TValue>(
			this ValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) >= 0);
		}

		/// <summary>
		///   Checks value is less
		/// </summary>
		public static ValidationCondition<TValue> IsLess<TValue>(
			this ValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) < 0);
		}

		/// <summary>
		///   Checks value is less or equal
		/// </summary>
		public static ValidationCondition<TValue> IsLessOrEqual<TValue>(
			this ValidationCondition<TValue> condition,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(comparable) <= 0);
		}

		/// <summary>
		///   Checks value is in range (min: inclusive, max: exclusive)
		/// </summary>
		public static ValidationCondition<TValue> IsInRange<TValue>(
			this ValidationCondition<TValue> condition,
			TValue min,
			TValue max)
			where TValue : IComparable<TValue>
		{
			return condition.Is(value => value.CompareTo(min) >= 0 && value.CompareTo(max) < 0);
		}

		/// <summary>
		///   Checks value is not in range (min: inclusive, max: exclusive)
		/// </summary>
		public static ValidationCondition<TValue> IsNotInRange<TValue>(
			this ValidationCondition<TValue> condition,
			TValue min,
			TValue max)
			where TValue : IComparable<TValue>
		{
			return condition.IsNot(value => value.CompareTo(min) >= 0 && value.CompareTo(max) < 0);
		}
	}
}