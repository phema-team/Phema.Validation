using System.Linq;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationPredicate<TValue> When<TValue>(
			this IValidationContext validationContext,
			string validationKey,
			TValue value)
		{
			// TODO: Prefix? Options? CreateSubPrefixValidationContext?
			// var fullValidationKey = validationContext.Prefix is null
			// ? validationKey
			// : $"{validationContext.Prefix}{ValidationDefaults.DefaultPrefixSeparator}{validationKey}";

			return new ValidationPredicate<TValue>(
				validationContext,
				// TODO: Prefix? Options? CreateSubPrefixValidationContext?
				validationKey,
				value);
		}
		
		public static IValidationPredicate<TValue> When<TValue>(
			this IValidationContext validationContext,
			TValue value)
		{
			return validationContext.When(ValidationDefaults.DefaultValidationKey, value);
		}

		public static bool IsValid(this IValidationContext validationContext, string validationKey = null)
		{
			return !validationContext.ValidationMessages
				.Any(m => (validationKey is null || m.Key == validationKey) 
					&& m.Severity >= validationContext.ValidationSeverity);
		}

		public static void EnsureIsValid(this IValidationContext validationContext, string validationKey = null)
		{
			if (!validationContext.IsValid(validationKey))
			{
				throw new ValidationContextException(validationContext);
			}
		}
	}
}