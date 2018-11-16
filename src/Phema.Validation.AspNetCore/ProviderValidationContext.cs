using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public class ProviderValidationContext : IValidationContext, IServiceProvider
	{
		private readonly IServiceProvider provider;
		private readonly IValidationContext validationContext;

		public ProviderValidationContext(IServiceProvider provider)
		{
			this.provider = provider;
			validationContext = new ValidationContext();
		}

		public ValidationSeverity Severity
		{
			get => validationContext.Severity;
			set => validationContext.Severity = value;
		}
		
		public IReadOnlyCollection<IValidationError> Errors => validationContext.Errors;
		
		public IValidationCondition<TValue> Validate<TValue>(ValidationKey key, TValue value)
		{
			var condition = validationContext.Validate(key, value);
			
			return new ProviderValidationCondition<TValue>(provider, condition);
		}

		public object GetService(Type serviceType)
		{
			return provider.GetService(serviceType);
		}
	}
	
	public class ProviderValidationCondition<TValue> : IValidationCondition<TValue>, IServiceProvider
	{
		private readonly IServiceProvider provider;
		private readonly IValidationCondition<TValue> condition;

		public ProviderValidationCondition(IServiceProvider provider, IValidationCondition<TValue> condition)
		{
			this.provider = provider;
			this.condition = condition;
		}

		public IValidationCondition<TValue> When(Condition<TValue> condition)
		{
			this.condition.When(condition);
			return this;
		}

		public IValidationError Add(Selector selector, object[] arguments, ValidationSeverity severity)
		{
			return condition.Add(selector, arguments, severity);
		}

		public object GetService(Type serviceType)
		{
			return provider.GetService(serviceType);
		}
	}
}