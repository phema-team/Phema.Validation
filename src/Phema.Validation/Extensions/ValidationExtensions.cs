using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	public static class ValidationExtensions
	{
		public static IServiceCollection AddPhemaValidation(this IServiceCollection services)
		{
			services.AddOptions<ValidationOptions>();
			services.TryAddScoped<IValidationContext, ValidationContext>();
			
			return services;
		}
	}
}
