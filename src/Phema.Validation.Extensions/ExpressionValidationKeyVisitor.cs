using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Validation
{
	internal class ExpressionValidationKeyVisitor : ExpressionVisitor
	{
		private readonly IList<string> keys = new List<string>();
		
		protected override Expression VisitMember(MemberExpression node)
		{
			if (!(node.Expression is ConstantExpression) && !CheckIfNestedConstant(node))
			{
				keys.Add(GetKey(node));
			}
			
			return base.VisitMember(node);
		}

		private bool CheckIfNestedConstant(MemberExpression expression)
		{
			switch (expression.Expression)
			{
				case ConstantExpression _:
					return true;
				case MemberExpression me:
					return CheckIfNestedConstant(me);
				default:
					return false;
			}
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			ProcessExpression(node.Right);

			return base.VisitBinary(node);
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			foreach (var expression in node.Arguments.Reverse())
			{
				ProcessExpression(expression);
			}
			
			return base.VisitMethodCall(node);
		}
		
		private void ProcessExpression(Expression expression)
		{
			switch (expression)
			{
				case ConstantExpression ce:
					keys.Add(ce.Value.ToString());
					break;
				case MemberExpression me:
					ProcessMemberRecursive(me, new List<MemberInfo>{ me.Member });
					break;
			}
		}

		private void ProcessMemberRecursive(MemberExpression expression, IList<MemberInfo> memberInfo)
		{
			switch (expression.Expression)
			{
				case ConstantExpression ce:

					var value = ce.Value;
					var type = value.GetType();
					
					foreach (var member in memberInfo.Reverse())
					{
						switch (member.MemberType)
						{
							case MemberTypes.Field:
								value = type.GetField(member.Name).GetValue(value);
								break;
							case MemberTypes.Property:
								value = type.GetProperty(member.Name).GetValue(value);
								break;
						}

						type = value.GetType();
					}
					
					keys.Add(value.ToString());
					break;
				
				case MemberExpression me:
					memberInfo.Add(me.Member);
					ProcessMemberRecursive(me, memberInfo);
					break;
			}
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