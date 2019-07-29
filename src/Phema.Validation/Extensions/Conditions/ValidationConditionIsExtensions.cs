using System;

namespace Phema.Validation
{
	public static class ValidationConditionIsExtensions
	{
		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> condition,
			Func<TValue, bool> predicate)
		{
			// Null or true
			if (condition.IsValid != false)
			{
				condition.IsValid = !predicate(condition.Value);
			}

			return condition;
		}

		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> condition,
			Func<bool> predicate)
		{
			return condition.Is(value => predicate());
		}

		public static IValidationCondition<TValue> IsNot<TValue>(
			this IValidationCondition<TValue> condition,
			Func<TValue, bool> predicate)
		{
			return condition.Is(value => !predicate(value));
		}

		public static IValidationCondition<TValue> IsNot<TValue>(
			this IValidationCondition<TValue> condition,
			Func<bool> predicate)
		{
			return condition.Is(value => !predicate());
		}

		public static IValidationCondition<TValue> IsNull<TValue>(
			this IValidationCondition<TValue> condition)
		{
			return condition.Is(value => value is null);
		}

		public static IValidationCondition<TValue> IsNotNull<TValue>(
			this IValidationCondition<TValue> condition)
		{
			return condition.Is(value => value != null);
		}

		public static IValidationCondition<TValue> IsEqual<TValue>(
			this IValidationCondition<TValue> condition,
			TValue expect)
		{
			return condition.Is(value => value?.Equals(expect) ?? expect?.Equals(null) ?? true);
		}

		public static IValidationCondition<TValue> IsNotEqual<TValue>(
			this IValidationCondition<TValue> condition,
			TValue expect)
		{
			return condition.Is(value => !(value?.Equals(expect) ?? expect?.Equals(null) ?? true));
		}
	}
}