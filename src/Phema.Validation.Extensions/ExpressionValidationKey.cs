using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Phema.Validation
{
	internal static class ExpressionCache
	{
		private static IDictionary<Expression, Func<object, object>> cache;

		public static Func<TModel, TProperty> GetFromExpression<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			if (!cache.TryGetValue(expression, out var factory))
			{
				var selector = expression.Compile();

				factory = m => selector((TModel)m);
				
				cache.Add(expression, factory);
			}
			
			return m => (TProperty)factory(m);
		}
		
		static ExpressionCache()
		{
			cache = new Dictionary<Expression, Func<object, object>>();
		}
	}
	
	internal class ExpressionValidationKey<TModel, TProperty> : ValidationKey
	{
		private readonly Expression<Func<TModel, TProperty>> expression;
		
		private ExpressionValidationKey(Expression<Func<TModel, TProperty>> expression) 
			: base(FormatKeyFromExpression(expression))
		{
			this.expression = expression;
		}
		
		public TProperty GetValue(TModel model)
		{
			return ExpressionCache.GetFromExpression(expression)(model);
		}

		private static string FormatKeyFromExpression(Expression<Func<TModel, TProperty>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException(nameof(expression));
			
			var visitor = new ExpressionValidationKeyVisitor();

			visitor.Visit(expression);

			return visitor.ToString();
		}
		
		public static implicit operator ExpressionValidationKey<TModel, TProperty>(
			Expression<Func<TModel, TProperty>> expression)
		{
			return new ExpressionValidationKey<TModel, TProperty>(expression);
		}
	}
}