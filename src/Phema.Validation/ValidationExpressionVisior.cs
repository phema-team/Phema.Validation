using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public interface IValidationExpressionVisior
	{
		string FromValidationPart(string? validationPath, string validationPart);

		string FromExpression(Expression expression);
	}

	internal sealed class ValidationExpressionVisior : IValidationExpressionVisior
	{
		private readonly ValidationOptions validationOptions;

		public ValidationExpressionVisior(IOptions<ValidationOptions> validationOptions)
		{
			this.validationOptions = validationOptions.Value;
		}

		public string FromValidationPart(string? validationPath, string validationPart)
		{
			return validationPath is null
				? validationPart
				: $"{validationPath}{validationOptions.ValidationPathSeparator}{validationPart}";
		}

		public string FromExpression(Expression expression)
		{
			return expression switch
			{
				BinaryExpression binaryExpression => VisitBinary(binaryExpression),
				MethodCallExpression methodCallExpression => VisitMethodCall(methodCallExpression),
				MemberExpression memberExpression => VisitMember(memberExpression),
				_ => throw new InvalidExpressionException()
			};
		}

		private string VisitBinary(BinaryExpression binaryExpression)
		{
			var validationPart = FromExpression(binaryExpression.Left);
			var argument = GetArgumentValue(binaryExpression.Right);

			return $"{validationPart}[{argument}]";
		}

		private string VisitMethodCall(MethodCallExpression methodCallExpression)
		{
			var validationPart = FromExpression(methodCallExpression.Object);
			var arguments = string.Join(", ", methodCallExpression.Arguments.Select(GetArgumentValue));

			return $"{validationPart}[{arguments}]";
		}

		private string VisitMember(MemberExpression memberExpression)
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
				ConstantExpression constantExpression => constantExpression.Value.ToString(),

				// TODO: More optimizations on simple usecases?
				// MemberExpression -> ConstantExpression = one reflection call

				_ => Expression.Lambda(expression).Compile().DynamicInvoke().ToString()
			};
		}
	}
}