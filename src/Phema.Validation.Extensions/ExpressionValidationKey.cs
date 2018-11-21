using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Validation
{
	internal static class ExpressionCache
	{
		private static IDictionary<Expression, Func<object, object>> cache;

		public static Func<TModel, TProperty> GetOrAdd<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
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
			return ExpressionCache.GetOrAdd(expression)(model);
		}

		private static string FormatKeyFromExpression(Expression<Func<TModel, TProperty>> expression)
		{
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

	internal class ExpressionValidationKeyVisitor : ExpressionVisitor
	{
		private readonly IList<string> keys = new List<string>();
		
		protected override Expression VisitMember(MemberExpression node)
		{
			if (node.Expression is ConstantExpression e)
			{
				
			}
			else
			{
				keys.Add(GetKey(node));
			}
			
			
			return base.VisitMember(node);
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			switch (node.Right)
			{
				case ConstantExpression ce:
					keys.Add(ce.Value.ToString());
					break;
				case MemberExpression me when me.Expression is ConstantExpression ce:
					keys.Add(ce.Value.GetType().GetField(me.Member.Name).GetValue(ce.Value).ToString());
					break;
			}

			return base.VisitBinary(node);
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			foreach (var argument in node.Arguments.Reverse())
			{
				switch (argument)
				{
					case ConstantExpression ce:
						keys.Add(ce.Value.ToString());
						break;
					
					case MemberExpression me when me.Expression is ConstantExpression ce:
						keys.Add(ce.Value.GetType().GetField(me.Member.Name).GetValue(ce.Value).ToString());
						break;
				}
			}
			
			return base.VisitMethodCall(node);
		}

		private string GetKey(MemberExpression memberExpression)
		{
			return memberExpression.Member.GetCustomAttribute<DataMemberAttribute>()?.Name
				?? memberExpression.Member.Name;
		}

		public override string ToString()
		{
			return string.Join(":", keys.Reverse());
		}
	}
}