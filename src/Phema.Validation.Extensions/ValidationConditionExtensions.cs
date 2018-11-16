namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> validationCondition,
			Condition condition)
		{
			return validationCondition.Is(value => condition());
		}
		
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

		public static IValidationError AddError(
			this IValidationCondition condition,
			Selector selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Error);
		}
		
		public static IValidationError AddWarning(
			this IValidationCondition condition,
			Selector selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Warning);
		}
		
		public static IValidationError AddInformation(
			this IValidationCondition condition,
			Selector selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Information);
		}
		
		public static IValidationError AddDebug(
			this IValidationCondition condition,
			Selector selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Debug);
		}
		
		public static IValidationError AddTrace(
			this IValidationCondition condition,
			Selector selector)
		{
			return condition.Add(selector, null, ValidationSeverity.Trace);
		}
		
		public static IValidationError AddError<TArgument>(
			this IValidationCondition builder,
			Selector<TArgument> selector,
			TArgument argument)
		{
			return builder.Add(() => selector(), new object [] { argument }, ValidationSeverity.Error);
		}
		
		public static IValidationError AddWarning<TArgument>(
			this IValidationCondition builder,
			Selector<TArgument> selector,
			TArgument argument)
		{
			return builder.Add(() => selector(), new object [] { argument }, ValidationSeverity.Warning);
		}
		
		public static IValidationError AddInformation<TArgument>(
			this IValidationCondition builder,
			Selector<TArgument> selector,
			TArgument argument)
		{
			return builder.Add(() => selector(), new object [] { argument }, ValidationSeverity.Information);
		}
		
		public static IValidationError AddDebug<TArgument>(
			this IValidationCondition builder,
			Selector<TArgument> selector,
			TArgument argument)
		{
			return builder.Add(() => selector(), new object [] { argument }, ValidationSeverity.Debug);
		}
		
		public static IValidationError AddTrace<TArgument>(
			this IValidationCondition builder,
			Selector<TArgument> selector,
			TArgument argument)
		{
			return builder.Add(() => selector(), new object [] { argument }, ValidationSeverity.Trace);
		}

		public static void Throw<TArgument>(
			this IValidationCondition builder,
			Selector<TArgument> selector,
			TArgument argument)
		{
			builder.Throw(() => selector(), new object[] { argument });
		}

		public static IValidationError AddError<TArgument1, TArgument2>(
			this IValidationCondition builder,
			Selector<TArgument1, TArgument2> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return builder.Add(() => selector(), new object[] { argument1, argument2 }, ValidationSeverity.Error);
		}
		
		public static IValidationError AddWarning<TArgument1, TArgument2>(
			this IValidationCondition builder,
			Selector<TArgument1, TArgument2> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return builder.Add(() => selector(), new object[] { argument1, argument2 }, ValidationSeverity.Warning);
		}
		
		public static IValidationError AddInformation<TArgument1, TArgument2>(
			this IValidationCondition builder,
			Selector<TArgument1, TArgument2> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return builder.Add(() => selector(), new object[] { argument1, argument2 }, ValidationSeverity.Information);
		}
		
		public static IValidationError AddDebug<TArgument1, TArgument2>(
			this IValidationCondition builder,
			Selector<TArgument1, TArgument2> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return builder.Add(() => selector(), new object[] { argument1, argument2 }, ValidationSeverity.Debug);
		}

		public static IValidationError AddTrace<TArgument1, TArgument2>(
			this IValidationCondition builder,
			Selector<TArgument1, TArgument2> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			return builder.Add(() => selector(), new object[] { argument1, argument2 }, ValidationSeverity.Trace);
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