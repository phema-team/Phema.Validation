using System;

using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ServiceCollectionExtensions
	{
		public static IMvcBuilder AddPhemaValidation(
			this IMvcBuilder builder,
			Action<IValidationConfiguration> configuration,
			Action<PhemaValidationOptions> options = null,
			Action<ExpressionPhemaValidationOptions> expressions = null)
		{
			builder.Services.AddPhemaValidation(configuration, options, expressions)
				.ConfigureOptions<PhemaValidationConfigureMvcOptions>();
			
			return builder;
		}
		
		public static IMvcCoreBuilder AddPhemaValidation(
			this IMvcCoreBuilder builder,
			Action<IValidationConfiguration> configuration,
			Action<PhemaValidationOptions> options = null,
			Action<ExpressionPhemaValidationOptions> expressions = null)
		{
			builder.Services.AddPhemaValidation(configuration, options, expressions)
				.ConfigureOptions<PhemaValidationConfigureMvcOptions>();
			
			return builder;
		}
	}
}