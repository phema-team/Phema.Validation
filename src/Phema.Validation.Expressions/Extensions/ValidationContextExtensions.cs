using System;
using System.Linq.Expressions;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationCondition<TValue> Validate<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var key = (ExpressionValidationKey<TModel, TValue>)expression;
			var value = key.GetValue(model);
			
			return validationContext.Validate(key, value);
		}
		
		public static IValidationCondition<TValue> Validate<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression,
			Func<TModel, TValue> selector)
		{
			var key = (ExpressionValidationKey<TModel, TValue>)expression;
			var value = selector(model);
			
			return validationContext.Validate(key, value);
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
			var key = (ExpressionValidationKey<TModel, TProperty>)expression;

			return validationContext.IsValid(key);
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
			var key = (ExpressionValidationKey<TModel, TProperty>)expression;

			validationContext.EnsureIsValid(key);
		}
	}
}