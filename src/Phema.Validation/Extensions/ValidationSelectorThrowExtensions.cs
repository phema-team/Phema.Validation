using System;

namespace Phema.Validation
{
	public static class ValidationSelectorThrowExtensions
	{
		public static void Throw(
			this IValidationSelector builder,
			Func<IValidationTemplate> selector,
			object[] arguments = null)
		{
			arguments = arguments ?? Array.Empty<object>();

			var error = builder.Add(selector, arguments, ValidationSeverity.Fatal);

			if (error != null)
			{
				throw new ValidationConditionException(error);
			}
		}

		public static void Throw(
			this IValidationSelector builder,
			Func<ValidationTemplate> selector)
		{
			builder.Throw(selector, null);
		}

		public static void Throw<TArgument>(
			this IValidationSelector builder,
			Func<ValidationTemplate<TArgument>> selector,
			TArgument argument)
		{
			builder.Throw(selector, new object[] { argument });
		}

		public static void Throw<TArgument1, TArgument2>(
			this IValidationSelector builder,
			Func<ValidationTemplate<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			builder.Throw(selector, new object[] { argument1, argument2 });
		}

		public static void Throw<TArgument1, TArgument2, TArgument3>(
			this IValidationSelector builder,
			Func<ValidationTemplate<TArgument1, TArgument2, TArgument3>> selector,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3)
		{
			builder.Throw(selector, new object[] { argument1, argument2, argument3 });
		}
	}
}