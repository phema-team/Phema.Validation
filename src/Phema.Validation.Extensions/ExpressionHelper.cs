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
	}
}