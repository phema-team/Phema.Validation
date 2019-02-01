using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Phema.Validation
{
	/// <summary>
	/// Используется для инкапсуляции работы с кешем скомпилированных выражений и преобразования выражений в строковые пути для ключей
	/// </summary>
	internal static class ExpressionHelper
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

		public static string FormatKeyFromExpression<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
			var visitor = new ExpressionValidationKeyVisitor();

			visitor.Visit(expression);

			return visitor.GetResult<TModel>();
		}
		
		static ExpressionHelper()
		{
			Cache = new ConcurrentDictionary<Expression, Func<object, object>>();
		}
	}
}