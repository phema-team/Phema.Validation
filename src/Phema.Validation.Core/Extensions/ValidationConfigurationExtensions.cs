namespace Phema.Validation
{
	public static class ValidationConfigurationExtensions
	{
		/// <summary>
		/// Расширяет функционал <see cref="IValidationConfiguration"/>, добавляя возможность типизировать модель
		/// </summary>
		/// <param name="configuration"></param>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TComponent"></typeparam>
		/// <returns></returns>
		public static IValidationConfiguration AddComponent<TModel, TComponent>(
			this IValidationConfiguration configuration)
				where TComponent : class, IValidationComponent<TModel>
		{
			return configuration.AddComponent<TComponent>();
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
			where TValidation : class, IValidation<TModel>
			where TComponent : class, IValidationComponent<TModel, TValidation>
		{
			return configuration.AddComponent<TModel, TComponent>()
				.AddValidation<TModel, TValidation>();
		}
	}
}