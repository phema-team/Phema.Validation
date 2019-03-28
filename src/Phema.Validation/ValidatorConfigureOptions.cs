using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Phema.Validation.Internal
{
	internal sealed class ValidatorConfigureOptions : IConfigureOptions<MvcOptions>
	{
		private readonly IServiceProvider serviceProvider;

		public ValidatorConfigureOptions(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public void Configure(MvcOptions options)
		{
			options.ModelValidatorProviders.Insert(0, new ModelValidatorProvider(serviceProvider));
			options.ModelMetadataDetailsProviders.Insert(0, new ValidationMetadataProvider());
		}
	}
}