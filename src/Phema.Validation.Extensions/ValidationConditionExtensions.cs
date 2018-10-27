using System;

namespace Phema.Validation{
	public static class ValidationConditionExtensions
	{
		public static IValidationCondition IsNot(this IValidationCondition validationCondition, Condition condition)
		{
			validationCondition.Is(() => !condition());
			
			return validationCondition;
		}

		public static void Throw(this IValidationCondition validationCondition, Func<ValidationMessage> selector, params object[] arguments)
		{
			var error = validationCondition.Add(selector, arguments);

			if (error != null)
			{
				throw new ValidationConditionException(error);
			}
		}
	}
}