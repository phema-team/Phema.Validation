using System.Linq;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		/// <summary>
		/// Specifies validation part with <see cref="TValue"/> value
		/// </summary>
		public static IValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string validationPart,
			TValue value)
		{
			var validationKey = JoinValidationPath(validationContext.ValidationPath, validationPart);

			return new ValidationCondition<TValue>(
				validationContext,
				validationKey,
				value);
		}

		/// <summary>
		/// Specifies validation part with object predicate with null value. Use with closures in conditions
		/// </summary>
		public static IValidationCondition<object> When(
			this IValidationContext validationContext,
			string validationPart)
		{
			return validationContext.When(validationPart, default(object));
		}

		/// <summary>
		/// Checks validation context for any detail with greater or equal severity. Additional filtering by validation key
		/// </summary>
		public static bool IsValid(this IValidationContext validationContext, string validationPart = null)
		{
			var validationKey = JoinValidationPath(validationContext.ValidationPath, validationPart);

			// TODO: Should severity be ignored when validationKey specified?
			return !validationContext.ValidationDetails
				.Any(m => (validationKey is null || m.ValidationKey == validationKey)
					&& m.ValidationSeverity >= validationContext.ValidationSeverity);
		}

		/// <summary>
		/// If validation context is not valid, throws <see cref="ValidationContextException"/>
		/// </summary>
		public static void EnsureIsValid(this IValidationContext validationContext, string validationPart = null)
		{
			if (!validationContext.IsValid(validationPart))
			{
				throw new ValidationContextException(validationContext);
			}
		}

		/// <summary>
		/// Creates new validation context with specified validation path
		/// </summary>
		public static IValidationContext CreateFor(this IValidationContext validationContext, string validationPart)
		{
			var validationPath = JoinValidationPath(validationContext.ValidationPath, validationPart);

			return new ValidationContext(
				new OptionsWrapper<ValidationOptions>(
					new ValidationOptions
					{
						ValidationPath = validationPath,
						ValidationSeverity = validationContext.ValidationSeverity,
						ValidationDetailsProvider = () => validationContext.ValidationDetails
					}));
		}

		private static string JoinValidationPath(string validationPath, string validationPart)
		{
			return validationPath is null
				? validationPart
				: $"{validationPath}{ValidationDefaults.PathSeparator}{validationPart}";
		}
	}
}