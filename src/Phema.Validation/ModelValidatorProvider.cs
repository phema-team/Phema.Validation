using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation.Internal
{
	internal sealed class ModelValidatorProvider : IModelValidatorProvider
	{
		private readonly IServiceProvider serviceProvider;

		public ModelValidatorProvider(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public void CreateValidators(ModelValidatorProviderContext context)
		{
			var item = new ValidatorItem
			{
				Validator = new ModelValidator(serviceProvider.CreateScope().ServiceProvider)
			};
			
			context.Results.Add(item);
		}
	}
}