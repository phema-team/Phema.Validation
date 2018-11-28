using System;

namespace Phema.Validation
{
	public static class ValidationConditionThrowExtensions
	{
		public static void Throw(
			this IValidationCondition builder,
			Func<ValidationMessage> selector,
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
			Func<ValidationMessage<TArgument>> selector,
			TArgument argument)
		{
			var error = builder.Add(
				selector, 
				new object[] { argument },
				ValidationSeverity.Fatal);

			if (error != null)
			{
				throw new ValidationConditionException(error);
			}
		}

		public static void Throw<TArgument1, TArgument2>(
			this IValidationCondition builder,
			Func<ValidationMessage<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			var error = builder.Add(
				selector, 
				new object[] { argument1, argument2 },
				ValidationSeverity.Fatal);

			if (error != null)
			{
				throw new ValidationConditionException(error);
			}
		}
	}
}