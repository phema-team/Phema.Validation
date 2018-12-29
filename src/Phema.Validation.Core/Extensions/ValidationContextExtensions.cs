using System;
using System.Linq;

namespace Phema.Validation
{
	public static class ValidationContextIsValidExtensions
	{
		public static IValidationCondition<TValue> Is<TValue>(
			this IValidationCondition<TValue> validationCondition,
			Func<bool> condition)
		{
			return validationCondition.Is(value => condition());
		}
		
		public static bool IsValid(this IValidationContext validationContext, IValidationKey validationKey)
		{
			return !validationContext
				.Errors
				.Where(error => error.Severity >= validationContext.Severity)
				.Any(error => validationKey == null || error.Key == validationKey.Key);
		}
	}
}