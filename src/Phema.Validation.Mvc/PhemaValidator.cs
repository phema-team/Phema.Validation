using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public class PhemaValidator : IModelValidator
	{
		private readonly IValidationContext validationContext;

		public PhemaValidator(IValidationContext validationContext)
		{
			this.validationContext = validationContext;
		}

		public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
		{
			var validationType = typeof(IValidation<>).MakeGenericType(context.Model.GetType());

			var validations = validationContext.GetServices(validationType);

			foreach (var validation in validations)
			{
				validation.GetType()
					.GetMethod(nameof(IValidation<object>.Validate), BindingFlags.Public | BindingFlags.Instance)
					.Invoke(validation, new [] { validationContext, context.Model });
			}

			return validationContext.Errors
				.Select(error => new ModelValidationResult(error.Key, error.Message));
		}
	}
}