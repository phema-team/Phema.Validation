using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Phema.Validation
{
	internal sealed class ProviderValidationContext : IValidationContext, IServiceProvider
	{
		private readonly IServiceProvider provider;
		private readonly IValidationContext validationContext;

		public ProviderValidationContext(IServiceProvider provider, IOptions<ValidationOptions> options)
		{
			this.provider = provider;
			validationContext = new ValidationContext(
				options.Value.Severity,
				TryGetCultureInfo(provider, options));
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

		private CultureInfo TryGetCultureInfo(IServiceProvider provider, IOptions<ValidationOptions> options)
		{
			var httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;

			if (httpContext == null)
			{
				return options.Value.CultureInfo;
			}

			var acceptLanguage = httpContext.Request.Headers[HeaderNames.AcceptLanguage];

			return acceptLanguage.Any()
				? CultureInfo.GetCultureInfo(acceptLanguage.Single())
				: options.Value.CultureInfo;
		}
	}
}