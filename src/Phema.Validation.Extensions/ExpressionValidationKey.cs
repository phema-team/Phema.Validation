using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Validation
{
	internal class ExpressionValidationKey<TModel, TProperty> : ValidationKey
	{
		private static Func<TModel, TProperty> factory;
		
		private readonly Expression<Func<TModel, TProperty>> expression;
		
		private ExpressionValidationKey(Expression<Func<TModel, TProperty>> expression) 
			: base(FormatKeyFromExpression(expression))
		{
			this.expression = expression;
		}
		
		public TProperty GetValue(TModel model)
		{
			if (factory == null)
			{
				var selector = expression.Compile();
				factory = m => selector(m);
			}
			
			return factory(model);
		}

		private static string FormatKeyFromExpression(Expression<Func<TModel, TProperty>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException(nameof(expression));

			switch (expression.Body)
			{
				case MemberExpression memberExpression:
					return GetKey(memberExpression);
				case UnaryExpression unaryExpression 
					when unaryExpression.Operand is MemberExpression memberExpression:
					return GetKey(memberExpression);
				default:
					throw new KeyNotFoundException();
			}
			
			string GetKey(MemberExpression memberExpression)
			{
				return memberExpression.Member.GetCustomAttribute<DataMemberAttribute>()?.Name
					?? memberExpression.Member.Name;
			}
		}
		
		public static implicit operator ExpressionValidationKey<TModel, TProperty>(
			Expression<Func<TModel, TProperty>> expression)
		{
			return new ExpressionValidationKey<TModel, TProperty>(expression);
		}
	}
}