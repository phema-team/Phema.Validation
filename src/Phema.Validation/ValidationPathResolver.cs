using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public interface IValidationPathResolver
	{
		string FromValidationPart(string? validationPath, string validationPart);

		string FromExpression(Expression expression);
	}

	internal sealed class ValidationPathResolver : IValidationPathResolver
	{
		private readonly ValidationOptions validationOptions;

		public ValidationPathResolver(IOptions<ValidationOptions> validationOptions)
		{
			this.validationOptions = validationOptions.Value;
		}

		public string FromValidationPart(string? validationPath, string validationPart)
		{
			return validationPath is null
				? validationPart
				: $"{validationPath}{validationOptions.ValidationPartSeparator}{validationPart}";
		}

		public string FromExpression(Expression expression)
		{
			return expression switch
			{
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
					FromValidationPart(FromExpression(innerMemberExpression), FromMemberExpression(memberExpression)),
				MethodCallExpression methodCallExpression =>
					FromValidationPart(FromExpression(methodCallExpression), FromMemberExpression(memberExpression)),
				BinaryExpression binaryExpression =>
					FromValidationPart(FromExpression(binaryExpression), FromMemberExpression(memberExpression)),

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
				MemberExpression member when member.Expression is MemberExpression => GetArgumentValue(member.Expression),
				MemberExpression member when member.Expression is ConstantExpression => GetArgumentValue(member.Expression),

				// TODO: More optimizations on simple usecases?

				_ => Expression.Lambda(expression).Compile().DynamicInvoke().ToString()
			};
		}
	}
}