using System;

namespace Phema.Validation
{
	public static class ValidationConditionIsExtensions
	{
		public static TValidationCondition Is<TValidationCondition>(
			this TValidationCondition condition,
			Func<bool> predicate)
			where TValidationCondition : IValidationCondition
		{
			// Null or true
			if (condition.IsValid != false)
			{
				condition.IsValid = !predicate();
			}

			return condition;
		}

		public static TValidationCondition IsNot<TValidationCondition>(
			this TValidationCondition condition,
			Func<bool> predicate)
			where TValidationCondition : IValidationCondition
		{
			return condition.Is(() => !predicate());
		}

		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> condition,
			Func<TValue, bool> predicate)
		{
			return condition.Is(() => predicate(condition.Value));
		}

		public static IValidationCondition<TValue> IsNot<TValue>(
			this IValidationCondition<TValue> condition,
			Func<TValue, bool> predicate)
		{
			return condition.IsNot(() => predicate(condition.Value));
		}

		public static IValidationCondition<TValue> IsNull<TValue>(
			this IValidationCondition<TValue> condition)
		{
			return condition.Is(value => value is null);
		}

		public static IValidationCondition<TValue> IsNotNull<TValue>(
			this IValidationCondition<TValue> condition)
		{
			return condition.IsNot(value => value is null);
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
			return condition.IsNot(value => value?.Equals(expect) ?? expect?.Equals(null) ?? true);
		}
	}
}