using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Основной метод конфигурации работы валидации и добавления зависимостей в контейнер
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static IServiceCollection AddPhemaValidation(
			this IServiceCollection services)
		{
			services.TryAddScoped<IValidationContext, ValidationContext>();
			return services;
		}
		
		/// <inheritdoc cref="AddPhemaValidation(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>
		public static IServiceCollection AddPhemaValidation(
			this IServiceCollection services,
			Action<IValidationConfiguration> configuration)
		{
			configuration.Invoke(new ValidationConfiguration(services));

			return services.AddPhemaValidation();
		}

		/// <inheritdoc cref="AddPhemaValidation(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>
		public static IServiceCollection AddPhemaValidation(
			this IServiceCollection services,
			Action<IValidationConfiguration> configuration,
			Action<PhemaValidationOptions> options)
		{
			return services.AddPhemaValidation(configuration)
				.Configure(options ?? (o => {}));
		}
	}
}