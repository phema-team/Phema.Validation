using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	public sealed class PhemaValidationConfigureMvcOptions : IConfigureOptions<MvcOptions>
	{
		private readonly IServiceProvider serviceProvider;

		public PhemaValidationConfigureMvcOptions(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}
		
		public void Configure(MvcOptions options)
		{
			options.ModelValidatorProviders.Add(new PhemaValidatorProvider(serviceProvider));
			options.ModelMetadataDetailsProviders.Add(new PhemaValidationMetadataProvider());
		}
	}
}