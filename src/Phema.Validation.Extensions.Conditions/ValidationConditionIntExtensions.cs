using System.ComponentModel.DataAnnotations;

namespace Phema.Validation
{
	public static class ValidationConditionIntExtensions
	{
		public static IValidationCondition<int> WhenGreater(
			this IValidationCondition<int> builder,
			int number)
		{
			return builder.When(value => value > number);
		}
		
		public static IValidationCondition<int> WhenLess(
			this IValidationCondition<int> builder,
			int number)
		{
			return builder.When(value => value < number);
		}
		
		public static IValidationCondition<int> WhenInRange(
			this IValidationCondition<int> builder,
			int min, 
			int max)
		{
			return builder.When(value => new RangeAttribute(min, max).IsValid(value));
		}
	}
}