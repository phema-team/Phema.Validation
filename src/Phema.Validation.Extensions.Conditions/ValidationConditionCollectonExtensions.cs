using System.Collections.Generic;

namespace Phema.Validation
{
	public static class ValidationConditionCollectonExtensions
	{
		public static IValidationCondition<ICollection<TElement>> WhenEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> builder)
		{
			return builder.When(value => value == null || value.Count == 0);
		}
		
		public static IValidationCondition<ICollection<TElement>> WhenNotEmpty<TElement>(
			this IValidationCondition<ICollection<TElement>> builder)
		{
			return builder.When(value => value != null && value.Count != 0);
		}
		
		public static IValidationCondition<ICollection<TElement>> WhenCount<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			int count)
		{
			return builder.When(value => value != null && value.Count == count);
		}
		
		public static IValidationCondition<ICollection<TElement>> WhenNotCount<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			int count)
		{
			return builder.When(value => value != null && value.Count != count);
		}
		
		public static IValidationCondition<ICollection<TElement>> WhenContains<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			TElement element)
		{
			return builder.When(value => value?.Contains(element) ?? false);
		}
		
		public static IValidationCondition<ICollection<TElement>> WhenNotContains<TElement>(
			this IValidationCondition<ICollection<TElement>> builder,
			TElement element)
		{
			return builder.When(value => !value?.Contains(element) ?? false);
		}
	}
}