using System;

namespace Phema.Validation
{
	public static class ValidationConditionIsExtensions
	{
		public static IValidationCondition<TValue> IsNot<TValue>(
			this IValidationCondition<TValue> builder,
			Func<bool> condition)
		{
			return builder.Is(value => !condition());
		}
		
		public static IValidationCondition<TValue> IsNot<TValue>(
			this IValidationCondition<TValue> builder,
			Func<TValue, bool> condition)
		{
			return builder.Is(value => !condition(value));
		}
		
		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> validationCondition,
			Func<bool> condition)
		{
			return validationCondition.Is(value => condition());
		}
	}
}