using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public static class ValidationConditionIntExtensions
	{
		public static IValidationCondition<int> IsGreater(
			this IValidationCondition<int> builder,
			int number)
		{
			return builder.Is(value => value > number);
		}
		
		public static IValidationCondition<int> IsLess(
			this IValidationCondition<int> builder,
			int number)
		{
			return builder.Is(value => value < number);
		}
		
		public static IValidationCondition<int> IsInRange(
			this IValidationCondition<int> builder,
			int min, 
			int max)
		{
			return builder.Is(value => new RangeAttribute(min, max).IsValid(value));
		}
	}
}