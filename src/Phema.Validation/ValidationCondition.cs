using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	internal sealed class ValidationCondition<TValue> : IValidationCondition<TValue>, IServiceProvider
	{
		private readonly IValidationKey key;
		private readonly TValue value;
		private readonly List<IValidationError> errors;
		private readonly IServiceProvider provider;
		private readonly IList<Func<TValue, bool>> conditions;

		public ValidationCondition(IValidationKey key, TValue value, List<IValidationError> errors, IServiceProvider provider)
		{
			this.key = key;
			this.value = value;
			this.errors = errors;
			this.provider = provider;
			conditions = new List<Func<TValue, bool>>();
		}

		public IValidationCondition<TValue> Is(Func<TValue, bool> condition)
		{
			conditions.Add(condition);
			return this;
		}

		public IValidationError Add(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity)
		{
			if (conditions.Count == 0)
			{
				return AddError(selector, arguments, severity);
			}

			foreach (var condition in conditions)
			{
				if (condition(value))
				{
					return AddError(selector, arguments, severity);
				}
			}

			return null;
		}
		
		private IValidationError AddError(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity)
		{
			var validationMessage = selector();

			var message = validationMessage?.GetMessage(arguments);
			
			var error = new ValidationError(key.Key, message, severity);
			errors.Add(error);
			return error;
		}

		object IServiceProvider.GetService(Type serviceType)
		{
			return provider.GetService(serviceType);
		}
	}
}