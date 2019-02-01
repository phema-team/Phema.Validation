using System;
using Microsoft.Extensions.DependencyInjection;

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
			this IServiceCollection services,
			Action<IValidationConfiguration> configuration = null,
			Action<ValidationOptions> options = null)
		{
			configuration?.Invoke(new ValidationConfiguration(services));

			return services
				.Configure(options ?? (o => {}))
				.AddScoped<IValidationContext, ValidationContext>();
		}
	}
}