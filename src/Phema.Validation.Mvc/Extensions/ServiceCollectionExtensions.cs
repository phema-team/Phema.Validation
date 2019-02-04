using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ServiceCollectionExtensions
	{
		public static IMvcBuilder AddPhemaValidationIntegration(this IMvcBuilder builder)
		{
			builder.Services.ConfigureOptions<PhemaValidationConfigureMvcOptions>();
			return builder;
		}
		
		public static IMvcCoreBuilder AddPhemaValidationIntegration(this IMvcCoreBuilder builder)
		{
			builder.Services.ConfigureOptions<PhemaValidationConfigureMvcOptions>();
			return builder;
		}
	}
}