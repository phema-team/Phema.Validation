using System.Linq;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		/// <summary>
		/// Specifies validation key with <see cref="TValue"/> value
		/// </summary>
		public static IValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string validationKey,
			TValue value)
		{
			if (validationContext.ValidationPath != null)
			{
				validationKey = $"{validationContext.ValidationPath}{ValidationDefaults.PathSeparator}{validationKey}";
			}

			return new ValidationCondition<TValue>(
				validationContext,
				validationKey,
				value);
		}

		/// <summary>
		/// Specifies validation key with object predicate with null value. Use with closures in conditions
		/// </summary>
		public static IValidationCondition<object> When(
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
				.Any(m => (validationKey is null || m.ValidationKey == validationKey)
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

		public static IValidationContext CreateFor(this IValidationContext validationContext, string validationPath)
		{
			if (validationContext.ValidationPath != null)
			{
				validationPath = $"{validationContext.ValidationPath}{ValidationDefaults.PathSeparator}{validationPath}";
			}

			return new ValidationContext(
				new OptionsWrapper<ValidationOptions>(
					new ValidationOptions
					{
						DefaultValidationPath = validationPath,
						DefaultValidationSeverity = validationContext.ValidationSeverity,
						DefaultValidationMessageFactory = () => validationContext.ValidationMessages
					}));
		}
	}
}