using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	/// <inheritdoc cref="IValidationCondition{TValue}"/>
	internal sealed class ValidationCondition<TValue> : IValidationCondition<TValue>
	{
		private readonly IValidationKey key;
		private readonly TValue value;
		private readonly IList<IValidationError> errors;
		private readonly IServiceProvider serviceProvider;
		private readonly IList<Func<TValue, bool>> selectors;

		public ValidationCondition(
			IServiceProvider serviceProvider,
			IValidationKey key,
			TValue value,
			IList<IValidationError> errors)
		{
			this.key = key;
			this.value = value;
			this.errors = errors;
			this.serviceProvider = serviceProvider;
			selectors = new List<Func<TValue, bool>>();
		}
		
		/// <inheritdoc cref="IValidationCondition{TValue}.Is"/>
		public IValidationCondition<TValue> Is(Func<TValue, bool> selector)
		{
			if (selector == null)
				throw new ArgumentNullException(nameof(selector));

			selectors.Add(selector);
			return this;
		}

		/// <inheritdoc cref="IValidationSelector.Add"/>
		public IValidationError Add(Func<IValidationTemplate> selector, object[] arguments, ValidationSeverity severity)
		{
			if (selector == null)
				throw new ArgumentNullException(nameof(selector));

			return selectors.All(condition => condition(value))
				? Add()
				: null;
			
			IValidationError Add()
			{
				if (selector == null)
					throw new ArgumentNullException(nameof(selector));

				var validationMessage = selector();

				if (validationMessage == null)
					throw new ArgumentNullException(nameof(validationMessage));

				var message = validationMessage.GetMessage(arguments);

				var error = new ValidationError(key.Key, message, severity);
				errors.Add(error);
				return error;
			}
		}
		
		public object GetService(Type serviceType)
		{
			return serviceProvider.GetService(serviceType);
		}
	}
}