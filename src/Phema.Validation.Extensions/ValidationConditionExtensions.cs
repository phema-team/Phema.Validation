namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition<TValue> When<TValue>(
			this IValidationCondition<TValue> validationCondition,
			Condition condition)
		{
			return validationCondition.When(value => condition());
		}
		
		public static void Throw(
			this IValidationCondition builder,
			Selector selector,
			object[] arguments = null)
		{
			var error = builder.Add(selector, arguments);

			if (error != null)
			{
				throw new ValidationConditionException(error);
			}
		}
		
		public static IValidationError Add<TArgument>(
			this IValidationCondition builder,
			Selector<TArgument> selector,
			TArgument argument)
		{
			return builder.Add(() => selector(), new object [] { argument });
		}

		public static void Throw<TArgument>(
			this IValidationCondition builder,
			Selector<TArgument> selector,
			TArgument argument)
		{
			builder.Throw(() => selector(), new object[] { argument });
		}

		public static IValidationError Add<TArgument1, TArgument2>(
			this IValidationCondition builder,
			Selector<TArgument1, TArgument2> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return builder.Add(() => selector(), new object[] { argument1, argument2 });
		}

		public static void Throw<TArgument1, TArgument2>(
			this IValidationCondition builder,
			Selector<TArgument1, TArgument2> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			builder.Throw(() => selector(), new object[] { argument1, argument2 });
		}
	}
}