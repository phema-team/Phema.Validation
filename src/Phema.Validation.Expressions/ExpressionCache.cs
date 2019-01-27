using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Phema.Validation
{
	internal static class ExpressionCache
	{
		private static IDictionary<Expression, Func<object, object>> Cache { get; }

		public static Func<TModel, TProperty> GetFromExpression<TModel, TProperty>(
			Expression<Func<TModel, TProperty>> expression)
		{
			if (!Cache.TryGetValue(expression, out var factory))
			{
				var selector = expression.Compile();

				factory = m => selector((TModel)m);

				Cache.Add(expression, factory);
			}

			return m => (TProperty)factory(m);
		}

		static ExpressionCache()
		{
			Cache = new Dictionary<Expression, Func<object, object>>();
		}
	}
}