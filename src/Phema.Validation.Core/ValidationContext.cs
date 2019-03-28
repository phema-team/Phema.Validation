using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public interface IValidationContext : IValidationSelector
	{
		ValidationSeverity Severity { get; }
		IReadOnlyCollection<IValidationError> Errors { get; }
		
		IValidationCondition<TValue> When<TValue>(IValidationKey key, TValue value);
	}
}

namespace Phema.Validation.Internal
{
	internal sealed class ValidationContext : IValidationContext
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
			return new ValidationCondition<TValue>(
				provider,
				key,
				value,
				error => errors.Add(error));
		}
		
		public IValidationError Add(Func<IServiceProvider, IValidationTemplate> selector, object[] arguments, ValidationSeverity severity)
		{
			return this.When().Add(selector, arguments, severity);
		}
	}
}