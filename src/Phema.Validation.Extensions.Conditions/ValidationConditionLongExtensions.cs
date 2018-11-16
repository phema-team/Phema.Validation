namespace Phema.Validation
{
	public static class ValidationConditionLongExtensions
	{
		public static IValidationCondition<long> WhenGreater(
			this IValidationCondition<long> builder,
			long number)
		{
			return builder.When(value => value > number);
		}
		
		public static IValidationCondition<long> WhenLess(
			this IValidationCondition<long> builder,
			long number)
		{
			return builder.When(value => value < number);
		}
		
		public static IValidationCondition<long> WhenInRange(
			this IValidationCondition<long> builder,
			long min, 
			long max)
		{
			return builder.When(value => value >= min && value <= max);
		}
	}
}