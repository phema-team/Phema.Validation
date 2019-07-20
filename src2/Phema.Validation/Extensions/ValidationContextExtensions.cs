using System.Linq;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		/// <summary>
		/// Specifies validation key with <see cref="TValue"/> value
		/// </summary>
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

		/// <summary>
		/// Specifies validation key with object predicate with null value. Use with closures in conditions
		/// </summary>
		public static IValidationPredicate<object> When(
			this IValidationContext validationContext,
			string validationKey)
		{
			return validationContext.When(validationKey, default(object));
		}

		/// <summary>
		/// Checks validation context for any message with greater or equal severity. Additional filtering by validation key
		/// </summary>
		public static bool IsValid(this IValidationContext validationContext, string validationKey = null)
		{
			return !validationContext.ValidationMessages
				.Any(m => (validationKey is null || m.Key == validationKey) 
					&& m.Severity >= validationContext.ValidationSeverity);
		}

		/// <summary>
		/// If validation context is not valid, throws <see cref="ValidationContextException"/>
		/// </summary>
		public static void EnsureIsValid(this IValidationContext validationContext, string validationKey = null)
		{
			if (!validationContext.IsValid(validationKey))
			{
				throw new ValidationContextException(validationContext);
			}
		}
	}
}