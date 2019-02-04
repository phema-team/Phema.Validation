using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	public static class ValidationConfigurationExtensions
	{
		/// <summary>
		/// Позволяет зарегистрировать новый компонент с <see cref="IValidationTemplate"/> для выбора сообщений валидации
		/// </summary>
		/// <typeparam name="TValidationComponent"></typeparam>
		/// <returns></returns>
		public static IValidationConfiguration AddComponent<TValidationComponent>(
			this IValidationConfiguration configuration)
				where TValidationComponent : class, IValidationComponent
		{
			configuration.Services.TryAddSingleton<TValidationComponent>();
			return configuration;
		}
	}
}