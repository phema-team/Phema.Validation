using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		/// <summary>
		///   Specifies validation part with object predicate without value. Use with closures in conditions
		/// </summary>
		public static IValidationCondition When(
			this IValidationContext validationContext,
			string validationPart)
		{
			var validationKey = validationContext.CombineValidationPath(validationPart);

			return new ValidationCondition(
				validationContext,
				validationKey);
		}

		/// <summary>
		///   Specifies validation part with provider of <see cref="TValue" /> value
		/// </summary>
		public static IValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string validationPart,
			Lazy<TValue> value)
		{
			var validationKey = validationContext.CombineValidationPath(validationPart);

			return new ValidationCondition<TValue>(
				validationContext,
				validationKey,
				value);
		}

		/// <summary>
		///   Specifies validation part with <see cref="TValue" /> value
		/// </summary>
		public static IValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string validationPart,
			TValue value)
		{
			return validationContext.When(validationPart, new Lazy<TValue>(() => value));
		}

		/// <summary>
		///   Checks validation context for any detail with greater or equal severity. Additional filtering by validation key
		/// </summary>
		public static bool IsValid(this IValidationContext validationContext, string? validationPart = null)
		{
			var validationDetails = validationContext.ValidationDetails
				.Where(detail => detail.ValidationSeverity >= validationContext.ValidationSeverity);

			if (validationPart != null)
			{
				var validationKey = validationContext.CombineValidationPath(validationPart);

				validationDetails = validationDetails.Where(detail => detail.ValidationKey == validationKey);
			}

			return !validationDetails.Any();
		}

		/// <summary>
		///   If validation context is not valid, throws <see cref="ValidationContextException" />
		/// </summary>
		public static void EnsureIsValid(this IValidationContext validationContext, string? validationPart = null)
		{
			if (!validationContext.IsValid(validationPart))
			{
				throw new ValidationContextException(validationContext);
			}
		}

		/// <summary>
		///   Creates new validation scope with specified validation path
		/// </summary>
		public static IValidationScope CreateScope(this IValidationContext validationContext, string validationPart)
		{
			var validationPath = validationContext.CombineValidationPath(validationPart);

			return new ValidationScope(
				validationContext,
				validationPath);
		}

		/// <summary>
		///   Combines validation contexts path with specified validation part
		/// </summary>
		public static string CombineValidationPath(this IValidationContext validationContext, string validationPart)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationPathResolver = serviceProvider.GetRequiredService<IValidationPathResolver>();

			return validationPathResolver.CombineValidationPath(validationContext.ValidationPath, validationPart);
		}
	}
}