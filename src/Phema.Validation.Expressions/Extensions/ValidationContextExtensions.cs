using System;
using System.Linq.Expressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationCondition<TProperty> When<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression)
		{
			var options = validationContext.GetRequiredService<IOptions<ExpressionPhemaValidationOptions>>().Value;

			var key = new ExpressionValidationKey<TModel, TProperty>(options, expression);
			var value = expression.Compile()(model);

			return validationContext.When(key, value);
		}

		public static IValidationCondition<TProperty> When<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression,
			Func<TModel, TProperty> selector)
		{
			var options = validationContext.GetRequiredService<IOptions<ExpressionPhemaValidationOptions>>().Value;
			
			var value = selector(model);

			return validationContext.When(new ExpressionValidationKey<TModel, TProperty>(options, expression), value);
		}

		public static bool IsValid<TModel>(
			this IValidationContext validationContext,
			Expression<Func<TModel, object>> expression)
		{
			return validationContext.IsValid(default, expression);
		}

		public static bool IsValid<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression)
		{
			var options = validationContext.GetRequiredService<IOptions<ExpressionPhemaValidationOptions>>().Value;

			return validationContext.IsValid(new ExpressionValidationKey<TModel, TProperty>(options, expression));
		}

		public static void EnsureIsValid<TModel>(
			this IValidationContext validationContext,
			Expression<Func<TModel, object>> expression)
		{
			validationContext.EnsureIsValid(default, expression);
		}

		public static void EnsureIsValid<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression)
		{
			var options = validationContext.GetRequiredService<IOptions<ExpressionPhemaValidationOptions>>().Value;
			
			var key = new ExpressionValidationKey<TModel, TProperty>(options, expression);

			validationContext.EnsureIsValid(key);
		}
	}
}