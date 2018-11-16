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
		
		public static bool IsValid(
			this IValidationContext validationContext, 
			ValidationSeverity? severity = null)
		{
			severity = severity ?? validationContext.Severity;
			
			return severity == null 
				|| !validationContext.Errors.Any(error => error.Severity >= severity);
		}

		public static bool IsValid(
			this IValidationContext validationContext, 
			ValidationKey key, 
			ValidationSeverity? severity = null)
		{
			severity = severity ?? validationContext.Severity;

			return severity == null 
				|| !validationContext.Errors.Any(error => error.Key == key.Key && error.Severity >= severity);
		}
		
		public static bool IsValid<TModel>(
			this IValidationContext validationContext,
			Expression<Func<TModel, object>> expression,
			ValidationSeverity? severity = null)
		{
			return validationContext.IsValid((ExpressionValidationKey<TModel, object>)expression, severity);
		}
		
		public static bool IsValid<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression,
			ValidationSeverity? severity = null)
		{
			return validationContext.IsValid((ExpressionValidationKey<TModel, TProperty>)expression, severity);
		}

		public static void EnsureIsValid(
			this IValidationContext validationContext,
			ValidationSeverity? severity = null)
		{
			if (!validationContext.IsValid(severity))
			{
				throw new ValidationContextException(validationContext.Errors);
			}
		}
	}
}