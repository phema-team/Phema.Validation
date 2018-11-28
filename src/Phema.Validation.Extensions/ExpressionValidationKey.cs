using System;
using System.Linq.Expressions;

namespace Phema.Validation
{
	internal sealed class ExpressionValidationKey<TModel, TProperty> : ValidationKey
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

			return visitor.Result;
		}
		
		public static implicit operator ExpressionValidationKey<TModel, TProperty>(
			Expression<Func<TModel, TProperty>> expression)
		{
			return new ExpressionValidationKey<TModel, TProperty>(expression);
		}
	}
}