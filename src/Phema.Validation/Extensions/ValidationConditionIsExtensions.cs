using System;

namespace Phema.Validation
{
	public static class ValidationConditionIsExtensions
	{
		/// <summary>
		///   Basic condition check without concrete value usage. Checks if predicate is valid
		/// </summary>
		public static TValidationCondition Is<TValidationCondition>(
			this TValidationCondition condition,
			Func<bool> predicate)
			where TValidationCondition : IValidationCondition
		{
			if (condition.Inverted)
			{
				if (condition.IsValid == false)
				{
					condition.IsValid = !predicate();
				}
			}
			else
			{
				// Null or false
				if (condition.IsValid != true)
				{
					condition.IsValid = !predicate();
				}
			}

			return condition;
		}

		/// <summary>
		///   Basic condition negative check without concrete value usage. Checks if predicate is not valid
		/// </summary>
		public static TValidationCondition IsNot<TValidationCondition>(
			this TValidationCondition condition,
			Func<bool> predicate)
			where TValidationCondition : IValidationCondition
		{
			return condition.Is(() => !predicate());
		}

		/// <summary>
		///   Checks if value is valid
		/// </summary>
		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> condition,
			Func<TValue, bool> predicate)
		{
			return condition.Is(() => predicate(condition.Value));
		}

		/// <summary>
		///   Checks if value is not valid
		/// </summary>
		public static IValidationCondition<TValue> IsNot<TValue>(
			this IValidationCondition<TValue> condition,
			Func<TValue, bool> predicate)
		{
			return condition.IsNot(() => predicate(condition.Value));
		}

		/// <summary>
		/// Should continue invalid checks to get IsValid back to true?
		/// </summary>
		public static IValidationCondition<TValue> Inverted<TValue>(
			this IValidationCondition<TValue> condition)
		{
			condition.Inverted = true;

			return condition;
		}
	}
}