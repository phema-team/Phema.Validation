using System;
using System.Linq;
using System.Linq.Expressions;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationCondition<TValue> When<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var key = (ExpressionValidationKey<TModel, TValue>)expression;
			var value = key.GetValue(model);
			
			return validationContext.When(key, value);
		}
		
		public static IValidationCondition<TValue> When<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression,
			Func<TModel, TValue> selector)
		{
			var key = (ExpressionValidationKey<TModel, TValue>)expression;
			var value = selector(model);
			
			return validationContext.When(key, value);
		}

		public static bool IsValid(this IValidationContext validationContext)
		{
			return !validationContext.Errors.Any(error => error.Severity >= validationContext.Severity);
		}
		
		public static bool IsValid<TModel>(
			this IValidationContext validationContext,
			Expression<Func<TModel, object>> expression)
		{
			var key = (ExpressionValidationKey<TModel, object>)expression;

			return !validationContext.Errors
				.Any(error => error.Key == key.Key && error.Severity >= validationContext.Severity);
		}
		
		public static bool IsValid<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression)
		{
			var key = (ExpressionValidationKey<TModel, TProperty>)expression;
			
			return !validationContext.Errors
				.Any(error => error.Key == key.Key && error.Severity >= validationContext.Severity);
		}

		public static void EnsureIsValid(
			this IValidationContext validationContext)
		{
			if (!validationContext.IsValid())
			{
				throw new ValidationContextException(validationContext.Errors);
			}
		}
	}
}