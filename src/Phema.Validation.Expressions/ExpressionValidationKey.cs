using System;
using System.Linq.Expressions;

namespace Phema.Validation
{
	/// <inheritdoc cref="IValidationKey"/>
	internal sealed class ExpressionValidationKey<TModel, TProperty> : IValidationKey
	{
		private string key;
		private readonly Expression<Func<TModel, TProperty>> expression;

		public ExpressionValidationKey(Expression<Func<TModel, TProperty>> expression)
		{
			this.expression = expression ?? throw new ArgumentNullException(nameof(expression));
		}

		/// <inheritdoc cref="IValidationKey.Key"/>
		public string Key => key ?? (key = ExpressionHelper.FormatKeyFromExpression(expression));

		public TProperty GetValue(TModel model)
		{
			return ExpressionHelper.GetFromExpression(expression)(model);
		}
	}
}