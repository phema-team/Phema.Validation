using System.Collections.Generic;

namespace Phema.Validation.Conditions
{
	public static class ValidationConditionCollectonExtensions
	{
		public static IValidationCondition<ICollection<TElement>> IsEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> condition)
		{
			return condition.Is(value => value is null || value.Count == 0);
		}

		public static IValidationCondition<ICollection<TElement>> IsNotEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> condition)
		{
			return condition.Is(value => value != null && value.Count != 0);
		}

		public static IValidationCondition<ICollection<TElement>> HasCount<TElement>(
			this IValidationCondition<ICollection<TElement>> condition,
			int count)
		{
			return condition.Is(value => value != null && value.Count == count);
		}

		public static IValidationCondition<ICollection<TElement>> NotHasCount<TElement>(
			this IValidationCondition<ICollection<TElement>> condition,
			int count)
		{
			return condition.Is(value => value != null && value.Count != count);
		}

		public static IValidationCondition<ICollection<TElement>> IsContains<TElement>(
			this IValidationCondition<ICollection<TElement>> condition,
			TElement element)
		{
			return condition.Is(value => value?.Contains(element) ?? false);
		}

		public static IValidationCondition<ICollection<TElement>> IsNotContains<TElement>(
			this IValidationCondition<ICollection<TElement>> condition,
			TElement element)
		{
			return condition.Is(value => !(value?.Contains(element) ?? false));
		}
	}
}