using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	public static class ValidationConfigurationExtensions
	{
		/// <summary>
		/// Позволяет зарегистрировать <see cref="IValidator{TModel}"/>, содержащий логику валидации <see cref="TModel"/>
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TValidation"></typeparam>
		/// <returns></returns>
		public static IValidationConfiguration AddValidation<TModel, TValidation>(
			this IValidationConfiguration configuration)
				where TValidation : class, IValidator<TModel>
		{
			configuration.Services.AddScoped<IValidator<TModel>, TValidation>();
			return configuration;
		}
		
		/// <summary>
		/// Расширяет функционал <see cref="IValidationConfiguration"/>, добавляя возможность типизировать модель и валидацию для этой модели
		/// </summary>
		/// <param name="configuration"></param>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TValidation"></typeparam>
		/// <typeparam name="TComponent"></typeparam>
		/// <returns></returns>
		public static IValidationConfiguration AddValidationComponent<TModel, TValidation, TComponent>(
			this IValidationConfiguration configuration)
				where TValidation : class, IValidator<TModel>
				where TComponent : class, IValidationComponent<TModel, TValidation>
		{
			return configuration.AddComponent<TModel, TComponent>()
				.AddValidation<TModel, TValidation>();
		}
	}
}