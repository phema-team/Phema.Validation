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
				validationKey = GetFullValidationPath(validationContext.ValidationPath, validationKey);
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
		/// Checks validation context for any detail with greater or equal severity. Additional filtering by validation key
		/// </summary>
		public static bool IsValid(this IValidationContext validationContext, string validationKey = null)
		{
			if (validationContext.ValidationPath != null)
			{
				validationKey = GetFullValidationPath(validationContext.ValidationPath, validationKey);
			}

			// TODO: Should severity be ignored when validationKey specified?
			// return validationContext.ValidationDetails
			// 	.Any(m => validationKey is null && m.ValidationSeverity >= validationContext.ValidationSeverity
			// 		|| m.ValidationKey == validationKey);

			return !validationContext.ValidationDetails
				.Any(m => (validationKey is null || m.ValidationKey == validationKey)
					&& m.ValidationSeverity >= validationContext.ValidationSeverity);
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

		/// <summary>
		/// Creates new validation context with specified validation path
		/// </summary>
		public static IValidationContext CreateFor(this IValidationContext validationContext, string validationPath)
		{
			if (validationContext.ValidationPath != null)
			{
				validationPath = GetFullValidationPath(validationContext.ValidationPath, validationPath);
			}

			return new ValidationContext(
				new OptionsWrapper<ValidationOptions>(
					new ValidationOptions
					{
						ValidationPath = validationPath,
						ValidationSeverity = validationContext.ValidationSeverity,
						ValidationDetailsFactory = () => validationContext.ValidationDetails
					}));
		}

		private static string GetFullValidationPath(string validationPath, string validationPart)
		{
			return $"{validationPath}{ValidationDefaults.PathSeparator}{validationPart}";
		}
	}
}