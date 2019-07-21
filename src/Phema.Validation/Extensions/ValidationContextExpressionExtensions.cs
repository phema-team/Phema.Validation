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
			var validationKey = GetValidationKeyFor(expression.Body);
			var value = expression.Compile().Invoke(model);

			return validationContext.When(validationKey, value);
		}
		
		/// <summary>
		/// Checks validation context for any detail with greater or equal severity for specified expression
		/// </summary>
		public static bool IsValid<TModel, TValue>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, TValue>> expression)
		{
			var validationKey = GetValidationKeyFor(expression.Body);

			return validationContext.IsValid(validationKey);
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
			var validationPath = GetValidationKeyFor(expression.Body);

			return validationContext.CreateFor(validationPath);
		}

		private static string GetValidationKeyFor(Expression expression)
		{
			switch (expression)
			{
				case BinaryExpression binaryExpression:
				{
					var memberName = GetValidationKeyFor(binaryExpression.Left);
					var arguments = GetValidationKeyFor(binaryExpression.Right);

					return $"{memberName}[{arguments}]";
				}

				case MethodCallExpression methodCallExpression:
				{
					var memberName = GetValidationKeyFor(methodCallExpression.Object);
					var arguments = string.Join(", ", methodCallExpression.Arguments.Select(GetValidationKeyFor));

					return $"{memberName}[{arguments}]";
				}

				case MemberExpression memberExpression:
					if (memberExpression.Expression is ConstantExpression constant)
					{
						return constant.Type
							.GetField(memberExpression.Member.Name)
							.GetValue(constant.Value)
							.ToString();
					}

					var dataMemberName = memberExpression.Member.GetCustomAttribute<DataMemberAttribute>()?.Name; 

					return dataMemberName ?? memberExpression.Member.Name;

				case ConstantExpression constantExpression:
					return constantExpression.Value.ToString();
			}

			throw new InvalidExpressionException();
		}
	}
}