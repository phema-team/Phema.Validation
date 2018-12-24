using System.Collections.Generic;

namespace Phema.Validation
{
	public static class ValidationConditionIsCollectonExtensions
	{
		public static IValidationCondition<ICollection<TElement>> IsEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> builder)
		{
			return builder.Is(value => value == null || value.Count == 0);
		}
		
		public static IValidationCondition<ICollection<TElement>> IsNotEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> builder)
		{
			return builder.Is(value => value != null && value.Count != 0);
		}
		
		public static IValidationCondition<ICollection<TElement>> HasCount<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			int count)
		{
			return builder.Is(value => value != null && value.Count == count);
		}
		
		public static IValidationCondition<ICollection<TElement>> NotHasCount<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			int count)
		{
			return builder.Is(value => value != null && value.Count != count);
		}
		
		public static IValidationCondition<ICollection<TElement>> IsContains<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			TElement element)
		{
			return builder.Is(value => value?.Contains(element) ?? false);
		}
		
		public static IValidationCondition<ICollection<TElement>> IsNotContains<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			TElement element)
		{
			return builder.Is(value => !(value?.Contains(element) ?? false));
		}
	}
}