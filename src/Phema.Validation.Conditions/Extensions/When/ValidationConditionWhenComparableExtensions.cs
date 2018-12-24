using System;

namespace Phema.Validation
{
	public static class ValidationConditionWhenComparableExtensions
	{
		public static IValidationCondition<TValue> WhenGreater<TValue>(
			this IValidationCondition<TValue> builder,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return builder.When(value => value.CompareTo(comparable) > 0);
		}
		
		public static IValidationCondition<TValue> WhenLess<TValue>(
			this IValidationCondition<TValue> builder,
			TValue comparable)
			where TValue : IComparable<TValue>
		{
			return builder.When(value => value.CompareTo(comparable) < 0);
		}
		
		public static IValidationCondition<TValue> WhenInRange<TValue>(
			this IValidationCondition<TValue> builder,
			TValue min, 
			TValue max)
			where TValue : IComparable<TValue>
		{
			return builder.When(value => value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0);
		}
	}
}