using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	internal sealed class ProviderValidationContext : IValidationContext, IServiceProvider
	{
		private readonly IServiceProvider provider;
		private readonly IValidationContext validationContext;

		public ProviderValidationContext(IServiceProvider provider, IOptions<ValidationOptions> options)
		{
			this.provider = provider;
			validationContext = new ValidationContext(options.Value.Severity);
		}

		public ValidationSeverity Severity => validationContext.Severity;

		public IReadOnlyCollection<IValidationError> Errors => validationContext.Errors;

		public IValidationCondition<TValue> When<TValue>(ValidationKey key, TValue value)
		{
			var condition = validationContext.When(key, value);

			return new ProviderValidationCondition<TValue>(provider, condition);
		}

		public object GetService(Type serviceType)
		{
			return provider.GetService(serviceType);
		}
	}
}