using System;
using System.Linq.Expressions;

namespace Phema.Validation
{
	internal sealed class ExpressionValidationKey<TModel, TProperty> : IValidationKey
	{
		private readonly Expression<Func<TModel, TProperty>> expression;

		internal ExpressionValidationKey(Expression<Func<TModel, TProperty>> expression, string separator)
		{
			if (expression == null)
				throw new ArgumentNullException(nameof(expression));

			this.expression = expression;
			Key = FormatKeyFromExpression(expression, separator);
		}

		public string Key { get; }

		public TProperty GetValue(TModel model)
		{
			return ExpressionCache.GetFromExpression(expression)(model);
		}

		private static string FormatKeyFromExpression(Expression<Func<TModel, TProperty>> expression, string separator)
		{
			var visitor = new ExpressionValidationKeyVisitor(separator);

			visitor.Visit(expression);

			return visitor.GetResult<TModel>();
		}
	}
}