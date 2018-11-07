using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Validation
{
	internal class ExpressionHelper
	{
		public static string GetKeyByMember<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
		{
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

		private static readonly Dictionary<(Type, Type), Func<object, object>> cache = new Dictionary<(Type, Type), Func<object, object>>();
		
		public static TProperty GetValueFromExpression<TModel, TProperty>(
			TModel model, 
			Expression<Func<TModel,TProperty>> expression)
		{
			if (!cache.TryGetValue((typeof(TModel), typeof(TProperty)), out var factory))
			{
				var selector = expression.Compile();

				factory = m => selector((TModel)m);
			}

			return (TProperty)factory(model);
		}
	}
}