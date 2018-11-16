using System;
using System.Linq;
using System.Linq.Expressions;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationCondition Validate(this IValidationContext validationContext)
		{
			return validationContext.Validate("", (object)null);
		}

		public static IValidationCondition<TValue> Validate<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var key = (ExpressionValidationKey<TModel, TValue>)expression;
			var value = key.GetValue(model);
			
			return validationContext.Validate(key, value);
		}
		
		public static bool IsValid(this IValidationContext validationContext)
		{
			return !validationContext.Errors.Any(error => error.Severity >= validationContext.Severity);
		}

		public static bool IsValid(
			this IValidationContext validationContext, 
			ValidationKey key)
		{
			return !validationContext.Errors
				.Any(error => error.Key == key.Key && error.Severity >= validationContext.Severity);
		}
		
		public static bool IsValid<TModel>(
			this IValidationContext validationContext,
			Expression<Func<TModel, object>> expression)
		{
			return validationContext.IsValid((ExpressionValidationKey<TModel, object>)expression);
		}
		
		public static bool IsValid<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression)
		{
			return validationContext.IsValid((ExpressionValidationKey<TModel, TProperty>)expression);
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