using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddValidation(
			this IServiceCollection services,
			Action<ValidationOptions> options = null)
		{
			options = options ?? (o => { });
			
			return services.AddScoped<IValidationContext, ValidationContext>()
				.Configure(options);
		}
	}
}