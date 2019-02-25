using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPhemaValidation(
			this IServiceCollection services,
			Action<IValidationConfiguration> configuration,
			Action<PhemaValidationOptions> options = null,
			Action<ExpressionPhemaValidationOptions> expressions = null)
		{
			return services.AddPhemaValidation(configuration, options)
				.Configure(expressions ?? (o => {}));
		}
	}
}