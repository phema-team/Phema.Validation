using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Phema.Validation.Extensions.Tests")]

namespace Phema.Validation
{
	internal static class ExpressionCache
	{
		private static readonly IDictionary<Expression, Func<object, object>> cache;

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
}