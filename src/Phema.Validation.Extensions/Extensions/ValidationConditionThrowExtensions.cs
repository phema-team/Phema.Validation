using System;

namespace Phema.Validation
{
	public static class ValidationConditionThrowExtensions
	{
		public static void Throw(
			this IValidationCondition builder,
			Func<IValidationMessage> selector,
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
			this IValidationCondition builder,
			Func<ValidationMessage> selector,
			object[] arguments = null)
		{
			arguments = arguments ?? Array.Empty<object>();
			
			builder.Throw((Func<IValidationMessage>)selector, arguments);
		}
		
		public static void Throw<TArgument>(
			this IValidationCondition builder,
			Func<ValidationMessage<TArgument>> selector,
			TArgument argument)
		{
			builder.Throw(selector, new object[] { argument });
		}

		public static void Throw<TArgument1, TArgument2>(
			this IValidationCondition builder,
			Func<ValidationMessage<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			builder.Throw(selector, new object[] { argument1, argument2 });
		}
		
		public static void Throw<TArgument1, TArgument2, TArgument3>(
			this IValidationCondition builder,
			Func<ValidationMessage<TArgument1, TArgument2, TArgument3>> selector,
			TArgument1 argument1,
			TArgument2 argument2,
			TArgument3 argument3)
		{
			builder.Throw(selector, new object[] { argument1, argument2, argument3 });
		}
	}
}