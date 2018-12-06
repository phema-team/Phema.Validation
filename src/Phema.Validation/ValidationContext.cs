using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	internal sealed class ValidationContext : IValidationContext, IServiceProvider
	{
		private readonly IServiceProvider provider;
		private readonly List<IValidationError> errors;
		
		public ValidationContext(IServiceProvider provider, IOptions<ValidationOptions> options)
		{
			this.provider = provider;
			Severity = options.Value.Severity;
			errors = new List<IValidationError>();
		}
		
		public ValidationSeverity Severity { get; }
		public IReadOnlyCollection<IValidationError> Errors => errors;
		
		public IValidationCondition<TValue> When<TValue>(IValidationKey key, TValue value)
		{
			return new ValidationCondition<TValue>(key, value, errors, provider);
		}

		object IServiceProvider.GetService(Type serviceType)
		{
			return provider.GetService(serviceType);
		}

		public IValidationError Add(Func<IValidationMessage> selector, object[] arguments, ValidationSeverity severity)
		{
			return When(new ValidationKey(""), (object)null)
				.Add(selector, arguments, severity);
		}
	}
}