using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ValidationExtensions
	{
		public static IServiceCollection AddValidation(this IServiceCollection services, Action<IValidationConfiguration> configuration)
		{
			if (!services.Any(s => s.ServiceType == typeof(IValidationContext)))
			{
				services.AddHttpContextAccessor();
				services.AddScoped<IValidationContext, ProviderValidationContext>();
				
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
			}
			
			configuration(new ValidationConfiguration(services));
			return services;
		}
	}
}
