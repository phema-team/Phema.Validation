using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Validation
{
	public static class ValidationContextExpressionExtensions
	{
		/// <summary>
		/// Specifies <see cref="TModel"/> with expression-based validation key
		/// </summary>
		public static IValidationCondition<TValue> When<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var value = expression.Compile().Invoke(model);
			var validationPart = GetValidationPart(expression.Body);

			return validationContext.When(validationPart, value);
		}
		
		/// <summary>
		/// Checks validation context for any detail with greater or equal severity for specified expression
		/// </summary>
		public static bool IsValid<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var validationPart = GetValidationPart(expression.Body);

			return validationContext.IsValid(validationPart);
		}

		/// <summary>
		/// Ensures that validation context has no details with greater or equal severity than ValidationContext.ValidationSeverity
		/// </summary>
		/// <exception cref="ValidationContextException">Throws when any detail has greater or equal severity</exception>
		public static void EnsureIsValid<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			if (!validationContext.IsValid(model, expression))
			{
				throw new ValidationContextException(validationContext);
			}
		}

		/// <summary>
		/// Creates new validation context with specified validation path
		/// </summary>
		public static IValidationContext CreateFor<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var validationPart = GetValidationPart(expression.Body);

			return validationContext.CreateFor(validationPart);
		}

		private static string GetValidationPart(Expression expression)
		{
			switch (expression)
			{
				case BinaryExpression binaryExpression:
				{
					var validationPart = GetValidationPart(binaryExpression.Left);
					var arguments = GetValidationPart(binaryExpression.Right);

					return $"{validationPart}[{arguments}]";
				}

				case MethodCallExpression methodCallExpression:
				{
					var validationPart = GetValidationPart(methodCallExpression.Object);
					var arguments = string.Join(", ", methodCallExpression.Arguments.Select(GetValidationPart));

					return $"{validationPart}[{arguments}]";
				}

				case MemberExpression memberExpression:
				{
					return memberExpression.Expression switch
					{
						ConstantExpression constantExpression => GetIndexFrom(memberExpression, constantExpression),
						MemberExpression innerMemberExpression => JoinValidationPart(innerMemberExpression, memberExpression),
						MethodCallExpression methodCallExpression => JoinValidationPart(methodCallExpression, memberExpression),
						BinaryExpression binaryExpression => JoinValidationPart(binaryExpression, memberExpression),

						_ => GetPathFor(memberExpression)
					};
				}

				case ConstantExpression constantExpression:
				{
					return constantExpression.Value.ToString();
				}
			}

			throw new InvalidExpressionException();
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

		private static string JoinValidationPart(Expression expression, MemberExpression memberExpression)
		{
			// Path sould be reversed, because reversed expression call
			return $"{GetValidationPart(expression)}{ValidationDefaults.PathSeparator}{GetPathFor(memberExpression)}";
		}

		private static string GetPathFor(MemberExpression memberExpression)
		{
			var dataMemberName = memberExpression.Member.GetCustomAttribute<DataMemberAttribute>()?.Name; 

			return dataMemberName ?? memberExpression.Member.Name;
		}
	}
}