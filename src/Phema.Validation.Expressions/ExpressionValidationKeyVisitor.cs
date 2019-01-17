using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Validation
{
	internal sealed class ExpressionValidationKeyVisitor : ExpressionVisitor
	{
		private readonly string separator;
		private readonly IList<string> keys = new List<string>();

		public ExpressionValidationKeyVisitor(string separator)
		{
			this.separator = separator;
		}

		public string GetResult<TModel>()
		{
			var prefix = typeof(TModel).GetCustomAttribute<DataContractAttribute>()?.Name;

			if (prefix != null)
				keys.Add(prefix);

			return string.Join(separator, keys.Reverse());
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			if (!(node.Expression is ConstantExpression) && !CheckIfNestedConstantExpression(node))
			{
				keys.Add(GetKey(node));
			}

			return base.VisitMember(node);
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
				case ConstantExpression constantExpression:
					keys.Add(constantExpression.Value.ToString());
					break;
				case MemberExpression memberExpression:
					ProcessMemberExpressionRecursive(memberExpression, new List<MemberInfo>
					{
						memberExpression.Member
					});
					break;
			}
		}

		private void ProcessMemberExpressionRecursive(MemberExpression expression, IList<MemberInfo> memberInfo)
		{
			switch (expression.Expression)
			{
				case ConstantExpression constantExpression:
					ProcessConstantExpression(constantExpression);
					break;

				case MemberExpression memberExpression:
					memberInfo.Add(memberExpression.Member);
					ProcessMemberExpressionRecursive(memberExpression, memberInfo);
					break;
			}

			void ProcessConstantExpression(ConstantExpression constantExpression)
			{
				var value = constantExpression.Value;
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
			}
		}

		private static bool CheckIfNestedConstantExpression(MemberExpression expression)
		{
			switch (expression.Expression)
			{
				case ConstantExpression _:
					return true;
				case MemberExpression me:
					return CheckIfNestedConstantExpression(me);
				default:
					return false;
			}
		}

		private static string GetKey(MemberExpression memberExpression)
		{
			return memberExpression.Member.GetCustomAttribute<DataMemberAttribute>()?.Name
				?? memberExpression.Member.Name;
		}
	}
}