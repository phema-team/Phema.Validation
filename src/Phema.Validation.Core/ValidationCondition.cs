using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	public interface IValidationCondition<out TValue> : IValidationSelector
	{
		IValidationCondition<TValue> Is(Func<TValue, bool> selector);
	}
}

namespace Phema.Validation.Internal
{
	internal sealed class ValidationCondition<TValue> : IValidationCondition<TValue>
	{
		private readonly TValue value;
		private readonly IValidationKey key;
		private readonly IServiceProvider provider;
		private readonly Action<IValidationError> action;
		private readonly IList<Func<TValue, bool>> selectors;

		public ValidationCondition(
			IServiceProvider provider,
			IValidationKey key,
			TValue value,
			Action<IValidationError> action)
		{
			this.key = key;
			this.value = value;
			this.action = action;
			this.provider = provider;
			selectors = new List<Func<TValue, bool>>();
		}

		public IValidationCondition<TValue> Is(Func<TValue, bool> selector)
		{
			if (selector is null)
				throw new ArgumentNullException(nameof(selector));

			selectors.Add(selector);
			return this;
		}

		public IValidationError Add(
			Func<IServiceProvider, IValidationTemplate> selector,
			object[] arguments,
			ValidationSeverity severity)
		{
			if (selector is null)
				throw new ArgumentNullException(nameof(selector));

			return selectors.All(condition => condition(value))
				? Add()
				: null;

			IValidationError Add()
			{
				if (selector is null)
					throw new ArgumentNullException(nameof(selector));

				var template = selector(provider);

				if (template is null)
					throw new ArgumentNullException(nameof(template));

				var error = new ValidationError(key.Key, template.GetMessage(arguments), severity);

				action(error);

				return error;
			}
		}
	}
}