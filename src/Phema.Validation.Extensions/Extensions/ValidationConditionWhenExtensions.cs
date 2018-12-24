using System;

namespace Phema.Validation
{
	public static class ValidationConditionWhenExtensions
	{
		public static IValidationCondition<TValue> WhenNot<TValue>(
			this IValidationCondition<TValue> builder,
			Func<bool> condition)
		{
			return builder.When(value => !condition());
		}
		
		public static IValidationCondition<TValue> WhenNot<TValue>(
			this IValidationCondition<TValue> builder,
			Func<TValue, bool> condition)
		{
			return builder.When(value => !condition(value));
		}
		
		public static IValidationCondition<TValue> When<TValue>(
			this IValidationCondition<TValue> validationCondition,
			Func<bool> condition)
		{
			return validationCondition.When(value => condition());
		}
	}
}