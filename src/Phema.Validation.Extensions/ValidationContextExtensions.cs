using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationCondition When(this IValidationContext validationContext)
		{
			return validationContext.When("");
		}

		public static IValidationCondition When<TModel>(
			this IValidationContext validationContext,
			TModel model,
			Expression<Func<TModel, object>> expression)
		{
			return validationContext.When(expression);
		}

		public static IValidationCondition When<TModel>(
			this IValidationContext validationContext,
			Expression<Func<TModel, object>> expression)
		{
			string key;

			switch (expression.Body)
			{
				case MemberExpression memberExpression:
					key = GetKey(memberExpression);
					break;
				case UnaryExpression unaryExpression 
						when unaryExpression.Operand is MemberExpression memberExpression:
					key = GetKey(memberExpression);
					break;
				default:
					throw new KeyNotFoundException();
			}

			return validationContext.When(key);

			string GetKey(MemberExpression memberExpression)
			{
				return memberExpression.Member.GetCustomAttribute<DataMemberAttribute>()?.Name
					?? memberExpression.Member.Name;
			}
		}

		public static bool IsValid(this IValidationContext validationContext)
		{
			return validationContext.Errors.Count == 0;
		}

		public static void EnsureIsValid(this IValidationContext validationContext)
		{
			if (!validationContext.IsValid())
			{
				throw new ValidationContextException(validationContext.Errors);
			}
		}
	}
}