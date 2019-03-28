using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Phema.Validation.Internal
{
	internal sealed class ModelValidator : IModelValidator
	{
		private readonly IServiceProvider serviceProvider;

		public ModelValidator(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
		{
			var validationContext = serviceProvider.GetRequiredService<IValidationContext>();
			var options = serviceProvider.GetRequiredService<IOptions<ValidatorOptions>>().Value;

			var dispatcher = options.Dispatchers[context.Model.GetType()];

			dispatcher(serviceProvider, validationContext, context.Model);

			return validationContext.Errors
				.Select(error => new ModelValidationResult(error.Key, error.Message));
		}
	}
}