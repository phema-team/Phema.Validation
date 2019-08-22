using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		/// <summary>
		/// Specifies validation part with object predicate without value. Use with closures in conditions
		/// </summary>
		public static IValidationCondition When(
			this IValidationContext validationContext,
			string validationPart)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationExpressionVisitor = serviceProvider.GetRequiredService<IValidationExpressionVisior>();

			var validationKey = validationExpressionVisitor
				.FromValidationPart(validationContext.ValidationPath, validationPart);

			return new ValidationCondition(
				validationContext,
				validationKey);
		}

		/// <summary>
		/// Specifies validation part with provider of <see cref="TValue"/> value
		/// </summary>
		public static IValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string validationPart,
			Func<TValue> provider)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationExpressionVisitor = serviceProvider.GetRequiredService<IValidationExpressionVisior>();

			var validationKey = validationExpressionVisitor
				.FromValidationPart(validationContext.ValidationPath, validationPart);

			return new ValidationCondition<TValue>(
				validationContext,
				validationKey,
				provider);
		}

		/// <summary>
		/// Specifies validation part with <see cref="TValue"/> value
		/// </summary>
		public static IValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string validationPart,
			TValue value)
		{
			return validationContext.When(validationPart, () => value);
		}

		/// <summary>
		/// Checks validation context for any detail with greater or equal severity. Additional filtering by validation key
		/// </summary>
		public static bool IsValid(this IValidationContext validationContext, string? validationPart = null)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationExpressionVisitor = serviceProvider.GetRequiredService<IValidationExpressionVisior>();

			var validationDetails = validationContext.ValidationDetails
				.Where(detail => detail.ValidationSeverity >= validationContext.ValidationSeverity);

			if (validationPart is { })
			{
				var validationKey = validationExpressionVisitor
					.FromValidationPart(validationContext.ValidationPath, validationPart);

				validationDetails = validationDetails.Where(detail => detail.ValidationKey == validationKey);
			}

			return !validationDetails.Any();
		}

		/// <summary>
		/// If validation context is not valid, throws <see cref="ValidationContextException"/>
		/// </summary>
		public static void EnsureIsValid(this IValidationContext validationContext, string? validationPart = null)
		{
			if (!validationContext.IsValid(validationPart))
			{
				throw new ValidationContextException(validationContext);
			}
		}

		/// <summary>
		/// Creates new validation scope with specified validation path
		/// </summary>
		public static IValidationScope CreateScope(this IValidationContext validationContext, string validationPart)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationExpressionVisitor = serviceProvider.GetRequiredService<IValidationExpressionVisior>();

			var validationPath = validationExpressionVisitor
				.FromValidationPart(validationContext.ValidationPath, validationPart);

			return new ValidationScope(
				validationContext,
				validationPath);
		}
	}
}