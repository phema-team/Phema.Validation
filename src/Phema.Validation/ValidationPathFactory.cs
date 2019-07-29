using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public interface IValidationPathFactory
	{
		string FromValidationPart(string validationPath, string validationPart);

		string FromExpression(Expression expression);
	}

	internal sealed class ValidationPathFactory : IValidationPathFactory
	{
		private readonly ValidationOptions validationOptions;

		public ValidationPathFactory(IOptions<ValidationOptions> validationOptions)
		{
			this.validationOptions = validationOptions.Value;
		}

		public string FromValidationPart(string validationPath, string validationPart)
		{
			return validationPath is null
				? validationPart
				: $"{validationPath}{validationOptions.ValidationPathSeparator}{validationPart}";
		}

		public string FromExpression(Expression expression)
		{
			switch (expression)
			{
				case BinaryExpression binaryExpression:
				{
					var validationPart = FromExpression(binaryExpression.Left);
					var arguments = FromExpression(binaryExpression.Right);

					return $"{validationPart}[{arguments}]";
				}

				case MethodCallExpression methodCallExpression:
				{
					var validationPart = FromExpression(methodCallExpression.Object);
					var arguments = string.Join(", ", methodCallExpression.Arguments.Select(FromExpression));

					return $"{validationPart}[{arguments}]";
				}

				case MemberExpression memberExpression:
				{
					return memberExpression.Expression switch
					{
						ConstantExpression constantExpression => GetIndexFrom(memberExpression, constantExpression),

						MemberExpression innerMemberExpression =>
							FromValidationPart(FromExpression(innerMemberExpression), FromMemberExpression(memberExpression)),
						MethodCallExpression methodCallExpression =>
							FromValidationPart(FromExpression(methodCallExpression), FromMemberExpression(memberExpression)),
						BinaryExpression binaryExpression =>
							FromValidationPart(FromExpression(binaryExpression), FromMemberExpression(memberExpression)),

						_ => FromMemberExpression(memberExpression)
					};
				}

				case ConstantExpression constantExpression:
				{
					return constantExpression.Value.ToString();
				}
			}

			throw new InvalidExpressionException();
		}

		private static string FromMemberExpression(MemberExpression memberExpression)
		{
			var dataMemberName = memberExpression.Member.GetCustomAttribute<DataMemberAttribute>()?.Name;

			return dataMemberName ?? memberExpression.Member.Name;
		}

		private static string GetIndexFrom(MemberExpression memberExpression, ConstantExpression constantExpression)
		{
			var type = memberExpression.Member.DeclaringType;

			return memberExpression.Member.MemberType switch
			{
				MemberTypes.Field => type
					.GetField(memberExpression.Member.Name,
						BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.GetValue(constantExpression.Value)
					.ToString(),

				MemberTypes.Property => type
					.GetProperty(memberExpression.Member.Name,
						BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.GetValue(constantExpression.Value)
					.ToString(),

				_ => throw new InvalidOperationException("Only fields and properties supported")
			};
		}
	}
}