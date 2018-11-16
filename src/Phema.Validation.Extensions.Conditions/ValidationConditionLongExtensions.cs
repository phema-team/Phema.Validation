namespace Phema.Validation
{
	public static class ValidationConditionLongExtensions
	{
		public static IValidationCondition<long> IsGreater(
			this IValidationCondition<long> builder,
			long number)
		{
			return builder.Is(value => value > number);
		}
		
		public static IValidationCondition<long> IsLess(
			this IValidationCondition<long> builder,
			long number)
		{
			return builder.Is(value => value < number);
		}
		
		public static IValidationCondition<long> IsInRange(
			this IValidationCondition<long> builder,
			long min, 
			long max)
		{
			return builder.Is(value => value >= min && value <= max);
		}
	}
}