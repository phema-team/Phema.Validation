using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	public static class ValidationExtensions
	{
		public static IServiceCollection AddPhemaValidation(this IServiceCollection services, Action<IValidationConfiguration> configuration)
		{
			services.AddPhemaValidation();
			
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

			services.TryAddSingleton<IValidationOutputFormatter, SimpleValidationOutputFormatter>();

			configuration(new ValidationConfiguration(services));

			return services;
		}
	}
}
