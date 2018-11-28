using System;

namespace Phema.Validation
{
	internal sealed class ProviderValidationCondition<TValue> : IValidationCondition<TValue>, IServiceProvider
	{
		private readonly IServiceProvider provider;
		private readonly IValidationCondition<TValue> condition;

		public ProviderValidationCondition(IServiceProvider provider, IValidationCondition<TValue> condition)
		{
			this.provider = provider;
			this.condition = condition;
		}

		public IValidationCondition<TValue> Is(Func<TValue, bool> condition)
		{
			this.condition.Is(condition);
			return this;
		}

		public IValidationError Add(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity)
		{
			return condition.Add(selector, arguments, severity);
		}

		public object GetService(Type serviceType)
		{
			return provider.GetService(serviceType);
		}
	}
}