namespace Phema.Validation
{
	/// <summary>
	/// Добавляет в контейнер компоненты валидации и шаблонов
	/// </summary>
	public interface IValidationConfiguration
	{
		/// <summary>
		/// Позволяет зарегистрировать новый компонент с <see cref="IValidationTemplate"/> для выбора сообщений валидации
		/// </summary>
		/// <typeparam name="TValidationComponent"></typeparam>
		/// <returns></returns>
		IValidationConfiguration AddComponent<TValidationComponent>()
			where TValidationComponent : class, IValidationComponent;

		/// <summary>
		/// Позволяет зарегистрировать <see cref="IValidation{TModel}"/>, содержащий логику валидации <see cref="TModel"/>
		/// </summary>
		/// <typeparam name="TModel"></typeparam>
		/// <typeparam name="TValidation"></typeparam>
		/// <returns></returns>
		IValidationConfiguration AddValidation<TModel, TValidation>()
			where TValidation : class, IValidation<TModel>;
	}
}