using System.Collections.Generic;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionCollectonExtensions
	{
		/// <summary>
		///   Checks collection is empty
		/// </summary>
		public static IValidationCondition<ICollection<TElement>> IsEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> condition)
		{
			return condition.Is(value => value is null || value.Count == 0);
		}

		/// <summary>
		///   Checks collection is not empty
		/// </summary>
		public static IValidationCondition<ICollection<TElement>> IsNotEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> condition)
		{
			return condition.IsNot(value => value is null || value.Count == 0);
		}

		/// <summary>
		///   Checks collection has count
		/// </summary>
		public static IValidationCondition<ICollection<TElement>> HasCount<TElement>(
			this IValidationCondition<ICollection<TElement>> condition,
			int count)
		{
			return condition.Is(value => value?.Count == count);
		}

		/// <summary>
		///   Checks collection has count not
		/// </summary>
		public static IValidationCondition<ICollection<TElement>> HasCountNot<TElement>(
			this IValidationCondition<ICollection<TElement>> condition,
			int count)
		{
			return condition.IsNot(value => value?.Count == count);
		}

		/// <summary>
		///   Checks collection is contains
		/// </summary>
		public static IValidationCondition<ICollection<TElement>> IsContains<TElement>(
			this IValidationCondition<ICollection<TElement>> condition,
			TElement element)
		{
			return condition.Is(value => value?.Contains(element) ?? false);
		}

		/// <summary>
		///   Checks collection is not contains
		/// </summary>
		public static IValidationCondition<ICollection<TElement>> IsNotContains<TElement>(
			this IValidationCondition<ICollection<TElement>> condition,
			TElement element)
		{
			return condition.IsNot(value => value?.Contains(element) ?? false);
		}
	}
}