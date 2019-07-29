using System;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ValidationContextExpressionExtensions
	{
		/// <summary>
		/// Specifies <see cref="TModel"/> with expression-based validation key
		/// </summary>
		public static IValidationCondition<TValue> When<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationPathFactory = serviceProvider.GetRequiredService<IValidationPathFactory>();

			var value = expression.Compile().Invoke(model);
			var validationPart = validationPathFactory.FromExpression(expression.Body);

			return validationContext.When(validationPart, value);
		}

		/// <summary>
		/// Checks validation context for any detail with greater or equal severity for specified expression
		/// </summary>
		public static bool IsValid<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationPathFactory = serviceProvider.GetRequiredService<IValidationPathFactory>();

			var validationPart = validationPathFactory.FromExpression(expression.Body);

			return validationContext.IsValid(validationPart);
		}

		/// <summary>
		/// Ensures that validation context has no details with greater or equal severity than ValidationContext.ValidationSeverity
		/// </summary>
		/// <exception cref="ValidationContextException">Throws when any detail has greater or equal severity</exception>
		public static void EnsureIsValid<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			if (!validationContext.IsValid(model, expression))
			{
				throw new ValidationContextException(validationContext);
			}
		}

		/// <summary>
		/// Creates new validation context with specified validation path
		/// </summary>
		public static IValidationContext CreateFor<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationPathFactory = serviceProvider.GetRequiredService<IValidationPathFactory>();

			var validationPart = validationPathFactory.FromExpression(expression.Body);

			return validationContext.CreateFor(validationPart);
		}
	}
}