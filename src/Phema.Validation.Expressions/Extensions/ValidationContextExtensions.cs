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
			var provider = (IServiceProvider)validationContext;
			var options = provider.GetRequiredService<IOptions<ValidationOptions>>().Value;
			
			var key = new ExpressionValidationKey<TModel, TProperty>(expression, options.Separator);
			var value = key.GetValue(model);
			
			return validationContext.When(key, value);
		}
		
		public static IValidationCondition<TProperty> When<TModel, TProperty>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TProperty>> expression,
			Func<TModel, TProperty> selector)
		{
			var provider = (IServiceProvider)validationContext;
			var options = provider.GetRequiredService<IOptions<ValidationOptions>>().Value;
			
			var key = new ExpressionValidationKey<TModel, TProperty>(expression, options.Separator);
			var value = selector(model);
			
			return validationContext.When(key, value);
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
			var provider = (IServiceProvider)validationContext;
			var options = provider.GetRequiredService<IOptions<ValidationOptions>>().Value;
			
			var key = new ExpressionValidationKey<TModel, TProperty>(expression, options.Separator);

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
			var provider = (IServiceProvider)validationContext;
			var options = provider.GetRequiredService<IOptions<ValidationOptions>>().Value;
			
			var key = new ExpressionValidationKey<TModel, TProperty>(expression, options.Separator);

			validationContext.EnsureIsValid(key);
		}
	}
}