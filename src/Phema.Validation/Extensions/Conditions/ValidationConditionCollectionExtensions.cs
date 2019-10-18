using System.Collections;
using System.Collections.Generic;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionCollectonExtensions
	{
		/// <summary>
		///   Checks collection is empty
		/// </summary>
		public static ValidationCondition<TCollection> IsEmpty<TCollection>(
			this ValidationCondition<TCollection> condition)
			where TCollection : ICollection
		{
			return condition.Is(value => value is null || value.Count == 0);
		}

		/// <summary>
		///   Checks collection is not empty
		/// </summary>
		public static ValidationCondition<TCollection> IsNotEmpty<TCollection>(
			this ValidationCondition<TCollection> condition)
			where TCollection : ICollection
		{
			return condition.IsNot(value => value is null || value.Count == 0);
		}

		/// <summary>
		///   Checks collection has count
		/// </summary>
		public static ValidationCondition<TCollection> HasCount<TCollection>(
			this ValidationCondition<TCollection> condition,
			int count)
			where TCollection : ICollection
		{
			return condition.Is(value => value?.Count == count);
		}

		/// <summary>
		///   Checks collection has count not
		/// </summary>
		public static ValidationCondition<TCollection> HasCountNot<TCollection>(
			this ValidationCondition<TCollection> condition,
			int count)
			where TCollection : ICollection
		{
			return condition.IsNot(value => value?.Count == count);
		}

		/// <summary>
		///   Checks collection is contains
		/// </summary>
		public static ValidationCondition<TCollection> IsContains<TCollection, TElement>(
			this ValidationCondition<TCollection> condition,
			TElement element)
			where TCollection : ICollection<TElement>
		{
			return condition.Is(value => value?.Contains(element) ?? false);
		}

		/// <summary>
		///   Checks collection is not contains
		/// </summary>
		public static ValidationCondition<TCollection> IsNotContains<TCollection, TElement>(
			this ValidationCondition<TCollection> condition,
			TElement element)
			where TCollection : ICollection<TElement>
		{
			return condition.IsNot(value => value?.Contains(element) ?? false);
		}
	}
}