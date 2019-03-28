using System;
using System.Linq.Expressions;

namespace Phema.Validation.Internal
{
	internal sealed class ExpressionValidationKey<TModel, TProperty> : IValidationKey
	{
		private string key;
		private readonly Expression<Func<TModel, TProperty>> expression;

		public ExpressionValidationKey(Expression<Func<TModel, TProperty>> expression)
		{
			this.expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

		public string Key => key ??= FormatKeyFromExpression(expression);

		private static string FormatKeyFromExpression(
			Expression<Func<TModel, TProperty>> expression)
		{
			var visitor = new ExpressionValidationKeyVisitor();

			visitor.Visit(expression);

			return visitor.GetResult<TModel>();
		}
	}
}