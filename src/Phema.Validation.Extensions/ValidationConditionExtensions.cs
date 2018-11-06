namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition IsNot(
			this IValidationCondition validationCondition,
			Condition condition)
		{
			validationCondition.Is(() => !condition());

			return validationCondition;
		}

		public static void Throw(
			this IValidationCondition validationCondition,
			Selector selector,
			params object[] arguments)
		{
			var error = validationCondition.Add(selector, arguments);

			if (error != null)
			{
				throw new ValidationConditionException(error);
			}
		}
		
		public static void Add<TArgument>(
			this IValidationCondition validationCondition,
			Selector<TArgument> selector,
			TArgument argument)
		{
			validationCondition.Add(() => selector(), argument);
		}

		public static void Throw<TArgument>(
			this IValidationCondition validationCondition,
			Selector<TArgument> selector,
			TArgument argument)
		{
			validationCondition.Throw(() => selector(), (object)argument);
		}

		public static void Add<TArgument1, TArgument2>(
			this IValidationCondition validationCondition,
			Selector<TArgument1, TArgument2> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			validationCondition.Add(() => selector(), argument1, argument2);
		}

		public static void Throw<TArgument1, TArgument2>(
			this IValidationCondition validationCondition,
			Selector<TArgument1, TArgument2> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			validationCondition.Throw(() => selector(), (object)argument1, (object)argument2);
		}
	}
}