using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	/// <inheritdoc cref="IValidationContext"/>
	internal sealed class ValidationContext : IValidationContext
	{
		private readonly IServiceProvider serviceProvider;

		public ValidationContext(IServiceProvider serviceProvider, IOptions<ValidationOptions> options)
		{
			Severity = options.Value.Severity;
			Errors = new List<IValidationError>();
			
			this.serviceProvider = serviceProvider;
		}
		
		/// <inheritdoc cref="IValidationContext.Severity"/>
		public ValidationSeverity Severity { get; }
		
		/// <inheritdoc cref="IValidationContext.Errors"/>
		public IReadOnlyCollection<IValidationError> Errors { get; }
		
		/// <inheritdoc cref="IValidationContext.When{TValue}"/>
		public IValidationCondition<TValue> When<TValue>(IValidationKey key, TValue value)
		{
			return new ValidationCondition<TValue>(serviceProvider, key, value, (List<IValidationError>)Errors);
		}
		
		/// <inheritdoc cref="IValidationSelector.Add"/>
		public IValidationError Add(Func<IServiceProvider, IValidationTemplate> selector, object[] arguments, ValidationSeverity severity)
		{
			return this.When().Add(selector, arguments, severity);
		}

		public object GetService(Type serviceType)
		{
			return serviceProvider.GetService(serviceType);
		}
	}
}