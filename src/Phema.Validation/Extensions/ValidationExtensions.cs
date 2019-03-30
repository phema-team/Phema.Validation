using System;
using Microsoft.Extensions.DependencyInjection;
using Phema.Validation.Internal;

namespace Phema.Validation
{
	public static class ValidationExtensions
	{
		public static IMvcBuilder AddValidation(
			this IMvcBuilder builder,
			Action<IValidationBuilder> validation = null,
			Action<ValidationOptions> options = null)
		{
			builder.Services.AddValidation(validation, options)
				.ConfigureOptions<ValidatorConfigureOptions>();

			return builder;
		}

		public static IMvcCoreBuilder AddValidation(
			this IMvcCoreBuilder builder,
			Action<IValidationBuilder> validation = null,
			Action<ValidationOptions> options = null)
		{
			builder.Services.AddValidation(validation, options)
				.ConfigureOptions<ValidatorConfigureOptions>();

			return builder;
		}
	}
}