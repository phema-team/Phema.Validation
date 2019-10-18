using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ValidationContextExpressionExtensions
	{
		/// <summary>
		///   Specifies <see cref="TModel" /> with expression-based validation key
		/// </summary>
		public static ValidationCondition<TValue> When<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationExpressionCache = serviceProvider.GetRequiredService<IValidationExpressionCache>();
			var validationPathResolver = serviceProvider.GetRequiredService<IValidationPathResolver>();

			var validationPart = validationPathResolver.FromExpression(expression.Body);

			return validationContext.When(
				validationPart,
				new Lazy<TValue>(() => validationExpressionCache.FromExpression(validationPart, expression).Invoke(model)));
		}

		/// <summary>
		///   Specifies <see cref="TModel" /> with expression-based validation key
		/// </summary>
		public static ValidationCondition<TValue> When<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression,
			Func<TValue> provider)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationPathResolver = serviceProvider.GetRequiredService<IValidationPathResolver>();

			var validationPart = validationPathResolver.FromExpression(expression.Body);

			return validationContext.When(
				validationPart,
				new Lazy<TValue>(provider));
		}

		/// <summary>
		///   Checks validation context for any detail with greater or equal severity for specified expression
		/// </summary>
		public static bool IsValid<TModel>(
			this IValidationContext validationContext,
			TModel model,
			params Expression<Func<TModel, object>>[] expressions)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationResolver = serviceProvider.GetRequiredService<IValidationPathResolver>();

			var validationParts = expressions.Select(e => validationResolver.FromExpression(e.Body)).ToArray();

			return validationContext.IsValid(validationParts);
		}

		/// <summary>
		///   Checks validation context is not valid
		/// </summary>
		public static bool IsNotValid<TModel>(
			this IValidationContext validationContext,
			TModel model,
			params Expression<Func<TModel, object>>[] expressions)
		{
			return !validationContext.IsValid(model, expressions);
		}

		/// <summary>
		///   Ensures that validation context has no details with greater or equal severity than
		///   ValidationContext.ValidationSeverity
		/// </summary>
		/// <exception cref="ValidationContextException">Throws when any detail has greater or equal severity</exception>
		public static void EnsureIsValid<TModel>(
			this IValidationContext validationContext,
			TModel model,
			params Expression<Func<TModel, object>>[] expressions)
		{
			if (!validationContext.IsValid(model, expressions))
			{
				throw new ValidationContextException(validationContext);
			}
		}

		/// <summary>
		///   Creates new validation scope with specified validation path from expression
		/// </summary>
		public static IValidationScope CreateScope<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression,
			ValidationSeverity? validationSeverity = null)
		{
			var serviceProvider = (IServiceProvider) validationContext;
			var validationResolver = serviceProvider.GetRequiredService<IValidationPathResolver>();

			var validationPart = validationResolver.FromExpression(expression.Body);

			return validationContext.CreateScope(validationPart, validationSeverity);
		}
	}
}