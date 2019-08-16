using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

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
			var serviceProvider = (IServiceProvider) validationContext;
			var validationExpressionVisitor = serviceProvider.GetRequiredService<IValidationExpressionVisior>();

			var validationKey = validationExpressionVisitor
				.FromValidationPart(validationContext.ValidationPath, validationPart);

			return new ValidationCondition<TValue>(
				validationContext,
				validationKey,
				value);
		}

		/// <summary>
		/// Specifies validation part with object predicate with null value. Use with closures in conditions
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
		/// Checks validation context for any detail with greater or equal severity. Additional filtering by validation key
		/// </summary>
		public static bool IsValid(this IValidationContext validationContext, string validationPart = null)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationExpressionVisitor = serviceProvider.GetRequiredService<IValidationExpressionVisior>();

			var validationKey = validationExpressionVisitor
				.FromValidationPart(validationContext.ValidationPath, validationPart);

			return !validationContext.ValidationDetails
				.Any(m => (validationPart is null || m.ValidationKey == validationKey)
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