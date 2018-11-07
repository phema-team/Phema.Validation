using System;
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

		public static IValidationCondition When<TModel>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, object>> expression)
		{
			return validationContext.When(expression);
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