using System;

namespace Phema.Validation
{
	internal class ProviderValidationCondition<TValue> : IValidationCondition<TValue>, IServiceProvider
	{
		private readonly IServiceProvider provider;
		private readonly IValidationCondition<TValue> condition;

		public ProviderValidationCondition(IServiceProvider provider, IValidationCondition<TValue> condition)
		{
			this.provider = provider;
			this.condition = condition;
		}

		public IValidationCondition<TValue> Is(Condition<TValue> condition)
		{
			this.condition.Is(condition);
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