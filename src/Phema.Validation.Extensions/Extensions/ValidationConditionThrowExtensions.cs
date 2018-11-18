namespace Phema.Validation
{
	public static class ValidationConditionThrowExtensions
	{
		public static void Throw(
			this IValidationCondition builder,
			Selector selector,
			object[] arguments = null)
		{
			var error = builder.Add(selector, arguments, ValidationSeverity.Fatal);

			if (error != null)
			{
				throw new ValidationConditionException(error);
			}
		}

		public static void Throw<TArgument>(
			this IValidationCondition builder,
			Selector<TArgument> selector,
			TArgument argument)
		{
			builder.Throw(() => selector(), new object[] { argument });
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