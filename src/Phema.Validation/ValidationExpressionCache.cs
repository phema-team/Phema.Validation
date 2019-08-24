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
		private readonly ConcurrentDictionary<(string, (Type, Type)), Delegate> cache;

		public ValidationExpressionCache()
		{
			cache = new ConcurrentDictionary<(string, (Type, Type)), Delegate>();
		}

		public Func<TModel, TValue> FromExpression<TModel, TValue>(
			string validationPart,
			Expression<Func<TModel, TValue>> expression)
		{
			if (!cache.TryGetValue((validationPart, (typeof(TModel), typeof(TValue))), out var factory))
			{
				cache.GetOrAdd((validationPart, (typeof(TModel), typeof(TValue))), factory = expression.Compile());
			}

			return (Func<TModel, TValue>) factory;
		}
	}
}