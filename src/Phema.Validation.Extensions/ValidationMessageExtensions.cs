using System;

namespace Phema.Validation
{
	public static class ValidationMessageExtensions
	{
		public static void Add<TArgument>(
			this IValidationCondition validationCondition,
			Func<ValidationMessage<TArgument>> selector,
			TArgument argument)
		{
			validationCondition.Add(() => selector(), argument);
		}

		public static void Throw<TArgument>(
			this IValidationCondition validationCondition,
			Func<ValidationMessage<TArgument>> selector,
			TArgument argument)
		{
			validationCondition.Throw(() => selector(), (object)argument);
		}

		public static void Add<TArgument1, TArgument2>(
			this IValidationCondition validationCondition,
			Func<ValidationMessage<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			validationCondition.Add(() => selector(), argument1, argument2);
		}

		public static void Throw<TArgument1, TArgument2>(
			this IValidationCondition validationCondition,
			Func<ValidationMessage<TArgument1, TArgument2>> selector,
			TArgument1 argument1,
			TArgument2 argument2)
		{
			validationCondition.Throw(() => selector(), (object)argument1, (object)argument2);
		}
	}
}