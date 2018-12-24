using System;

namespace Phema.Validation
{
	public static class ValidationConditionIsComparableExtensions
	{
		public static IValidationCondition<TValue> IsGreater<TValue>(
			this IValidationCondition<TValue> builder,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return builder.Is(value => value.CompareTo(comparable) > 0);
		}
		
		public static IValidationCondition<TValue> IsLess<TValue>(
			this IValidationCondition<TValue> builder,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return builder.Is(value => value.CompareTo(comparable) < 0);
		}
		
		public static IValidationCondition<TValue> IsInRange<TValue>(
			this IValidationCondition<TValue> builder,
			TValue min, 
			TValue max)
			where TValue : IComparable<TValue>
		{
			return builder.Is(value => value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0);
		}
	}
}