using System;

namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		public static void Throw(
			this IValidationCondition builder,
			Func<IValidationMessage> selector,
			object[] arguments = null)
		{
			var error = builder.Add(selector, arguments, ValidationSeverity.Fatal);

			if (error != null)
			{
				throw new ValidationConditionException(error);
			}
		}
	}
}