using System;
using System.Linq;
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
				if (!options.Filters.Any(x => x is ValidationExceptionFilter))
				{
					options.Filters.Add<ValidationExceptionFilter>();
				}

				if (!options.Filters.Any(x => x is ValidationFilter))
				{
					options.Filters.Add<ValidationFilter>();
				}
			});

			configuration(new ValidationConfiguration(services));
		}
	}
}
