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

		public static IValidationError Add<TValidationComponent>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage> selector)
			where TValidationComponent : ValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.Add(() => message);
		}
		
		public static IValidationError Add<TValidationComponent, TArgument>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument>> selector, TArgument argument)
			where TValidationComponent : ValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.Add(() => message, argument);
		}
		
		public static IValidationError Add<TValidationComponent, TArgument1, TArgument2>(this IValidationCondition condition, Func<TValidationComponent, ValidationMessage<TArgument1, TArgument2>> selector, TArgument1 argument1, TArgument2 argument2)
			where TValidationComponent : ValidationComponent
		{
			var provider = (IServiceProvider)condition;

			var component = provider.GetRequiredService<TValidationComponent>();

			var message = selector(component);

			return condition.Add(() => message, argument1, argument2);
		}
	}
}
