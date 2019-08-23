using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Phema.Validation
{
	public interface IValidationExpressionCache
	{
		Func<TModel, TValue> FromExpression<TModel, TValue>(
			string validationPart,
			Expression<Func<TModel, TValue>> expression);
	}

	internal sealed class ValidationExpressionCache : IValidationExpressionCache
	{
		private readonly ConcurrentDictionary<(string, string), Delegate> cache;

		public ValidationExpressionCache()
		{
			cache = new ConcurrentDictionary<(string, string), Delegate>();
		}
		
		public Func<TModel, TValue> FromExpression<TModel, TValue>(
			string validationPart,
			Expression<Func<TModel, TValue>> expression)
		{
			var expressionPart = expression.ToString();

			if (!cache.TryGetValue((validationPart, expressionPart), out var factory))
			{
				cache.GetOrAdd((validationPart, expressionPart), factory = expression.Compile());
			}

			return (Func<TModel, TValue>) factory;
		}
	}
}