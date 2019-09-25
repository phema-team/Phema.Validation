using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionIsExtensions
	{
		/// <summary>
		///   Checks values has a value
		/// </summary>
		public static IValidationCondition<TValue> IsIn<TValue>(
			this IValidationCondition<TValue> condition,
			IEnumerable<TValue> values)
		{
			return condition.Is(value => values.Contains(condition.Value));
		}

		/// <summary>
		///   Checks values has a value
		/// </summary>
		public static IValidationCondition<TValue> IsIn<TValue>(
			this IValidationCondition<TValue> condition,
			params TValue[] values)
		{
			return condition.Is(value => values.Contains(condition.Value));
		}

		/// <summary>
		///   Checks values has a value
		/// </summary>
		public static IValidationCondition<TValue> IsNotIn<TValue>(
			this IValidationCondition<TValue> condition,
			IEnumerable<TValue> values)
		{
			return condition.IsNot(value => values.Contains(condition.Value));
		}

		/// <summary>
		///   Checks values has a value
		/// </summary>
		public static IValidationCondition<TValue> IsNotIn<TValue>(
			this IValidationCondition<TValue> condition,
			params TValue[] values)
		{
			return condition.IsNot(value => values.Contains(condition.Value));
		}

		/// <summary>
		///   Checks value is null
		/// </summary>
		public static IValidationCondition<TValue> IsNull<TValue>(this IValidationCondition<TValue> condition)
		{
			return condition.Is(value => value is null);
		}

		/// <summary>
		///   Checks value is not null
		/// </summary>
		public static IValidationCondition<TValue> IsNotNull<TValue>(this IValidationCondition<TValue> condition)
		{
			return condition.IsNot(value => value is null);
		}

		/// <summary>
		///   Checks value is equal using Equals method
		/// </summary>
		public static IValidationCondition<TValue> IsEqual<TValue>(
			this IValidationCondition<TValue> condition,
			TValue expect)
		{
			return condition.Is(value => value?.Equals(expect) ?? expect is null);
		}

		/// <summary>
		///   Checks value is not equal using Equals method
		/// </summary>
		public static IValidationCondition<TValue> IsNotEqual<TValue>(
			this IValidationCondition<TValue> condition,
			TValue expect)
		{
			return condition.IsNot(value => value?.Equals(expect) ?? expect is null);
		}

		/// <summary>
		///   Checks value is type of
		/// </summary>
		public static IValidationCondition<TValue> IsType<TValue>(
			this IValidationCondition<TValue> condition,
			Type type)
		{
			return condition.Is(value => value?.GetType() == type);
		}

		/// <summary>
		///   Checks value is type of <see cref="TType"/>
		/// </summary>
		public static IValidationCondition<object> IsType<TType>(
			this IValidationCondition<object> condition,
			Action<IValidationCondition<TType>> typed = null)
		{
			if (condition.Value is TType)
			{
				var typedCondition = new ValidationCondition<TType>(
					condition.ValidationContext,
					condition.ValidationKey,
					new Lazy<TType>(() => (TType)condition.Value)); 

				typed?.Invoke(typedCondition);

				condition.IsValid = typedCondition.IsValid ?? false;
			}
			else
			{
				condition.IsValid = true;
			}

			return condition;
		}
	}
}