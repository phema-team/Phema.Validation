using System.Linq;
using System.Collections.Generic;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionIsExtensions
	{
		/// <summary>
		///   Checks values has a value
		/// </summary>
		public static ValidationCondition<TValue> IsIn<TValue>(
			this ValidationCondition<TValue> condition,
			IEnumerable<TValue> values)
		{
			return condition.Is(value => values.Contains(condition.Value));
		}

		/// <summary>
		///   Checks values has a value
		/// </summary>
		public static ValidationCondition<TValue> IsIn<TValue>(
			this ValidationCondition<TValue> condition,
			params TValue[] values)
		{
			return condition.Is(value => values.Contains(condition.Value));
		}

		/// <summary>
		///   Checks values has a value
		/// </summary>
		public static ValidationCondition<TValue> IsNotIn<TValue>(
			this ValidationCondition<TValue> condition,
			IEnumerable<TValue> values)
		{
			return condition.IsNot(value => values.Contains(condition.Value));
		}

		/// <summary>
		///   Checks values has a value
		/// </summary>
		public static ValidationCondition<TValue> IsNotIn<TValue>(
			this ValidationCondition<TValue> condition,
			params TValue[] values)
		{
			return condition.IsNot(value => values.Contains(condition.Value));
		}

		/// <summary>
		///   Checks value is null
		/// </summary>
		public static ValidationCondition<TValue> IsNull<TValue>(this ValidationCondition<TValue> condition)
		{
			return condition.Is(value => value is null);
		}

		/// <summary>
		///   Checks value is not null
		/// </summary>
		public static ValidationCondition<TValue> IsNotNull<TValue>(this ValidationCondition<TValue> condition)
		{
			return condition.IsNot(value => value is null);
		}

		/// <summary>
		///   Checks value is equal using Equals method
		/// </summary>
		public static ValidationCondition<TValue> IsEqual<TValue>(
			this ValidationCondition<TValue> condition,
			TValue expect)
		{
			return condition.Is(value => value?.Equals(expect) ?? expect is null);
		}

		/// <summary>
		///   Checks value is not equal using Equals method
		/// </summary>
		public static ValidationCondition<TValue> IsNotEqual<TValue>(
			this ValidationCondition<TValue> condition,
			TValue expect)
		{
			return condition.IsNot(value => value?.Equals(expect) ?? expect is null);
		}
	}
}