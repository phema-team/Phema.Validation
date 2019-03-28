using System.Linq;
using Phema.Validation.Internal;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationSelector When(this IValidationContext validationContext)
		{
			return validationContext.When(string.Empty);
		}

		public static IValidationSelector When(this IValidationContext validationContext, string key)
		{
			return validationContext.When<object>(new ValidationKey(key), null);
		}

		public static IValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string key,
			TValue value)
		{
			return validationContext.When(new ValidationKey(key), value);
		}

		public static bool IsValid(this IValidationContext validationContext, string key)
		{
			return validationContext.IsValid(new ValidationKey(key));
		}

		public static void EnsureIsValid(this IValidationContext validationContext, string key)
		{
			validationContext.EnsureIsValid(new ValidationKey(key));
		}

		public static bool IsValid(this IValidationContext validationContext, IValidationKey validationKey = null)
		{
			return !validationContext.Errors
				.Where(error => error.Severity >= validationContext.Severity)
				.Any(error => validationKey == null || error.Key == validationKey.Key);
		}

		public static void EnsureIsValid(this IValidationContext validationContext, IValidationKey validationKey = null)
		{
			if (!validationContext.IsValid(validationKey))
			{
				throw new ValidationContextException(validationContext.Errors, validationContext.Severity);
			}
		}
	}
}