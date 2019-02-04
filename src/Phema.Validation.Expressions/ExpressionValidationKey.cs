using System;
using System.Linq.Expressions;

namespace Phema.Validation
{
	/// <inheritdoc cref="IValidationKey"/>
	internal sealed class ExpressionValidationKey<TModel, TProperty> : IValidationKey
	{
		private string key;
		private readonly ExpressionPhemaValidationOptions options;
		private readonly Expression<Func<TModel, TProperty>> expression;

		public ExpressionValidationKey(ExpressionPhemaValidationOptions options, Expression<Func<TModel, TProperty>> expression)
		{
			this.options = options;
			this.expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

		/// <inheritdoc cref="IValidationKey.Key"/>
		public string Key => key ?? (key = FormatKeyFromExpression(options, expression));
		
		private static string FormatKeyFromExpression(
			ExpressionPhemaValidationOptions options,
			Expression<Func<TModel, TProperty>> expression)
		{
			var visitor = new ExpressionValidationKeyVisitor(options);

			visitor.Visit(expression);

			return visitor.GetResult<TModel>();
		}
	}
}