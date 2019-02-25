using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Phema.Validation
{
	internal sealed class PhemaValidationConfigureMvcOptions : IConfigureOptions<MvcOptions>
	{
		private readonly IServiceProvider serviceProvider;

		public PhemaValidationConfigureMvcOptions(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}
		
		public void Configure(MvcOptions options)
		{
			options.ModelValidatorProviders.Insert(0, new PhemaValidatorProvider(serviceProvider));
			options.ModelMetadataDetailsProviders.Insert(0, new PhemaValidationMetadataProvider());
		}
	}
}