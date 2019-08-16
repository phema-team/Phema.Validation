using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionEnumerableExtensions
	{
		/// <summary>
		/// Checks enumerable has elements
		/// </summary>
		public static IValidationCondition<IEnumerable<TElement>> IsAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> condition)
		{
			return condition.Is(value => value?.Any() ?? false);
		}

		/// <summary>
		/// Checks enumerable has elements with predicate
		/// </summary>
		public static IValidationCondition<IEnumerable<TElement>> IsAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> condition,
			Func<TElement, bool> predicate)
		{
			return condition.Is(value => value?.Any(predicate) ?? false);
		}

		/// <summary>
		/// Checks enumerable has no elements
		/// </summary>
		public static IValidationCondition<IEnumerable<TElement>> IsNotAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> condition)
		{
			return condition.IsNot(value => value?.Any() ?? false);
		}

		/// <summary>
		/// Checks enumerable has no elements with predicate
		/// </summary>
		public static IValidationCondition<IEnumerable<TElement>> IsNotAny<TElement>(
			this IValidationCondition<IEnumerable<TElement>> condition,
			Func<TElement, bool> predicate)
		{
			return condition.IsNot(value => value?.Any(predicate) ?? false);
		}

		/// <summary>
		/// Checks all elements suites for predicate
		/// </summary>
		public static IValidationCondition<IEnumerable<TElement>> IsAll<TElement>(
			this IValidationCondition<IEnumerable<TElement>> condition,
			Func<TElement, bool> predicate)
		{
			return condition.Is(value => value?.All(predicate) ?? false);
		}

		/// <summary>
		/// Checks not all elements suites for predicate
		/// </summary>
		public static IValidationCondition<IEnumerable<TElement>> IsNotAll<TElement>(
			this IValidationCondition<IEnumerable<TElement>> condition,
			Func<TElement, bool> predicate)
		{
			return condition.IsNot(value => value?.All(predicate) ?? false);
		}
	}
}