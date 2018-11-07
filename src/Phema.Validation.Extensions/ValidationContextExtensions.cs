using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationCondition When(this IValidationContext validationContext)
		{
			return validationContext.When("");
		}

		public static IValueValidationCondition<TProperty> When<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression)
		{
			var key = ExpressionHelper.GetKeyByMember(expression);
			var value = ExpressionHelper.GetValueFromExpression(model, expression);
			
			var validationCondition = validationContext.When(key);
			
			return new ValueValidationCondition<TProperty>(validationCondition, value);
		}

		public static IValidationCondition When<TModel>(
			this IValidationContext validationContext,
			Expression<Func<TModel, object>> expression)
		{
			var key = ExpressionHelper.GetKeyByMember(expression);
			
			return validationContext.When(key);
		}

		public static bool IsValid(this IValidationContext validationContext)
		{
			return validationContext.Errors.Count == 0;
		}

		public static bool IsValid(this IValidationContext validationContext, string key)
		{
			return !validationContext.Errors.Any(error => error.Key == key);
		}
		
		public static bool IsValid<TModel>(
			this IValidationContext validationContext, 
			Expression<Func<TModel, object>> expression)
		{
			var key = ExpressionHelper.GetKeyByMember(expression);

			return validationContext.IsValid(key);
		}

		public static void EnsureIsValid(this IValidationContext validationContext)
		{
			if (!validationContext.IsValid())
			{
				throw new ValidationContextException(validationContext.Errors);
			}
		}
	}
}