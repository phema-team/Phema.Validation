using System;
using System.Collections.Generic;
using System.Linq;

namespace Phema.Validation
{
	internal sealed class ValidationCondition<TValue> : IValidationCondition<TValue>, IServiceProvider
	{
		private readonly IValidationKey key;
		private readonly TValue value;
		private readonly List<IValidationError> errors;
		private readonly IServiceProvider provider;
		private readonly IList<Func<TValue,  bool>> conditions;

		public ValidationCondition(
			IValidationKey key, 
			TValue value, 
			List<IValidationError> errors,
			IServiceProvider provider)
		{
			this.key = key;
			this.value = value;
			this.errors = errors;
			this.provider = provider;
			conditions = new List<Func<TValue, bool>>();
		}

		public IValidationCondition<TValue> Is(Func<TValue, bool> condition)
		{
			if (condition == null)
				throw new ArgumentNullException(nameof(condition));
			
			conditions.Add(condition);
			return this;
		}

		public IValidationError Add(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity)
		{
			if (selector == null)
				throw new ArgumentNullException(nameof(selector));

			return conditions.All(condition => condition(value)) 
				? AddError(selector, arguments, severity) 
				: null;
		}
		
		private IValidationError AddError(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity)
		{
			if(selector == null)
				throw new ArgumentNullException(nameof(selector));
			
			var validationMessage = selector();

			if (validationMessage == null)
				throw new ArgumentNullException(nameof(validationMessage));
			
			var message = validationMessage.GetMessage(arguments);
			
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