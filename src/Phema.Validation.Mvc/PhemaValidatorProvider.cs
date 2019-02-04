using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public class PhemaValidatorProvider : IModelValidatorProvider
	{
		private readonly IServiceProvider serviceProvider;

		public PhemaValidatorProvider(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public void CreateValidators(ModelValidatorProviderContext context)
		{
			context.Results.Add(new ValidatorItem
			{
				Validator = new PhemaValidator(
					serviceProvider.CreateScope()
						.ServiceProvider
						.GetRequiredService<IValidationContext>())
			});
		}
	}
}