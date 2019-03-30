using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Phema.Validation.Internal;

namespace Phema.Validation
{
	public static class ValidationExtensions
	{
		public static IServiceCollection AddValidation(
			this IServiceCollection services,
			Action<IValidationBuilder> validation = null,
			Action<ValidationOptions> options = null)
		{
			services.Configure(options ?? (o => { }));
			validation?.Invoke(new ValidationBuilder(services));
			services.TryAddScoped<IValidationContext, ValidationContext>();

			return services;
		}
	}
}