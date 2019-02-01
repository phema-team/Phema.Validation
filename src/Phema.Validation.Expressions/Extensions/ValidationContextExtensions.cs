using System;
using System.Linq.Expressions;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationCondition<TProperty> When<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression)
		{
			var key = new ExpressionValidationKey<TModel, TProperty>(expression);
			var value = key.GetValue(model);

			return validationContext.When(key, value);
		}

		public static IValidationCondition<TProperty> When<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression,
			Func<TModel, TProperty> selector)
		{
			var value = selector(model);

			return validationContext.When(new ExpressionValidationKey<TModel, TProperty>(expression), value);
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
			return validationContext.IsValid(new ExpressionValidationKey<TModel, TProperty>(expression));
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
			var key = new ExpressionValidationKey<TModel, TProperty>(expression);

			validationContext.EnsureIsValid(key);
		}
	}
}