using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Phema.Validation
{
	public static class ValidationConfigurationExtensions
	{
		/// <summary>
		/// Расширяет функционал <see cref="IValidationConfiguration"/>, добавляя возможность типизировать модель
		/// </summary>
		/// <param name="configuration"></param>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TValidationComponent"></typeparam>
		/// <returns></returns>
		public static IValidationConfiguration AddComponent<TModel, TValidationComponent>(
		this IValidationConfiguration configuration)
			where TValidationComponent : class, IValidationComponent<TModel>
		{
			return configuration.AddComponent<TValidationComponent>();
		}
	}
}