using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public interface IValidationPathResolver
	{
		string CombineValidationPath(string? validationPath, string validationPart);

		string FromExpression(Expression expression);
	}

	internal sealed class ValidationPathResolver : IValidationPathResolver
	{
		private readonly ValidationOptions validationOptions;

		public ValidationPathResolver(IOptions<ValidationOptions> validationOptions)
		{
			this.validationOptions = validationOptions.Value;
		}

		public string CombineValidationPath(string? validationPath, string validationPart)
		{
			return validationPath is null
				? validationPart
				: $"{validationPath}{validationOptions.ValidationPartSeparator}{validationPart}";
		}

		public string FromExpression(Expression expression)
		{
			return expression switch
			{
				UnaryExpression unaryExpression => FromExpression(unaryExpression.Operand),
				BinaryExpression binaryExpression => FromBinary(binaryExpression),
				MethodCallExpression methodCallExpression => FromMethodCall(methodCallExpression),
				MemberExpression memberExpression => FromMember(memberExpression),
				_ => throw new InvalidExpressionException()
			};
		}

		private string FromBinary(BinaryExpression binaryExpression)
		{
			var validationPart = FromExpression(binaryExpression.Left);
			var argument = GetArgumentValue(binaryExpression.Right);

			return $"{validationPart}[{argument}]";
		}

		private string FromMethodCall(MethodCallExpression methodCallExpression)
		{
			var validationPart = FromExpression(methodCallExpression.Object);
			var arguments = string.Join(", ", methodCallExpression.Arguments.Select(GetArgumentValue));

			return $"{validationPart}[{arguments}]";
		}

		private string FromMember(MemberExpression memberExpression)
		{
			return memberExpression.Expression switch
			{
				MemberExpression innerMemberExpression =>
					CombineValidationPath(FromExpression(innerMemberExpression), FromMemberExpression(memberExpression)),
				MethodCallExpression methodCallExpression =>
					CombineValidationPath(FromExpression(methodCallExpression), FromMemberExpression(memberExpression)),
				BinaryExpression binaryExpression =>
					CombineValidationPath(FromExpression(binaryExpression), FromMemberExpression(memberExpression)),

				_ => FromMemberExpression(memberExpression)
			};
		}

		private string FromMemberExpression(MemberExpression memberExpression)
		{
			return validationOptions.ValidationPartResolver(memberExpression.Member);
		}

		private static string GetArgumentValue(Expression expression)
		{
			return expression switch
			{
				ConstantExpression constant => constant.Value.ToString(),

				MemberExpression member 
					when member.Expression is MemberExpression => GetChainedArgumentValue(member),

				MemberExpression member
					when member.Expression is ConstantExpression => GetChainedArgumentValue(member),

				_ => throw new InvalidExpressionException($"Unsupported expression type: {expression}")
			};
		}

		private static string GetChainedArgumentValue(MemberExpression memberExpression)
		{
			var members = new List<MemberInfo>();
			var value = GetChainedArgumentValue(members, memberExpression);

			foreach (var member in members)
			{
				value = member switch
				{
					PropertyInfo propertyInfo => propertyInfo.GetMethod.Invoke(value, Array.Empty<object>()),
					FieldInfo fieldInfo => fieldInfo.GetValue(value),
					_ => value
				};
			}

			return value.ToString();
		}

		private static object GetChainedArgumentValue(List<MemberInfo> members, MemberExpression expression)
		{
			var value = expression.Expression switch
			{
				ConstantExpression constantExpression => constantExpression.Value,
				MemberExpression memberExpression => GetChainedArgumentValue(members, memberExpression),
				_ => throw new InvalidExpressionException($"Unsupported expression type: {expression.Expression}")
			};

			members.Add(expression.Member);

			return value;
		}
	}
}