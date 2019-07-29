using System;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds <see cref="IValidationContext"/> dependency and configures <see cref="ValidationOptions"/>
		/// </summary>
		public static IServiceCollection AddValidation(
			this IServiceCollection services,
			Action<ValidationOptions> options = null)
		{
			options ??= o => { };

			return services.AddScoped<IValidationContext, ValidationContext>()
				.AddSingleton<IValidationExpressionVisior, ValidationExpressionVisior>()
				.Configure(options);
		}
	}
}