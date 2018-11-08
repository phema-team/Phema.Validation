using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	public static class ValidationExtensions
	{
		public static void AddValidation(this IServiceCollection services, Action<IValidationConfiguration> configuration)
		{
			services.TryAddScoped<IValidationContext, ValidationContext>();
			services.Configure<MvcOptions>(options =>
			{
				options.Filters.Add<ValidationExceptionFilter>();
				options.Filters.Add<ValidationFilter>();
			});

			configuration(new ValidationConfiguration(services));
		}
	}
}
