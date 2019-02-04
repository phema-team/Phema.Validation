namespace Phema.Validation
{
	/// <summary>
	/// Компонент для валидации <see cref="TModel"/>
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public interface IValidation<in TModel>
	{
		/// <summary>
		/// Метод, в котором должна быть описана логика валидации <see cref="TModel"/>
		/// </summary>
		/// <param name="validationContext"></param>
		/// <param name="model"></param>
		void Validate(IValidationContext validationContext, TModel model);
	}
	
	/// <summary>
	/// Типизированный <see cref="TModel"/> и <see cref="IValidation{TModel}"/> компонент, содержащий один и более <see cref="IValidationTemplate"/> для валидации
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	/// <typeparam name="TValidation"></typeparam>
	public interface IValidationComponent<TModel, TValidation> : IValidationComponent<TModel>
		where TValidation : class, IValidation<TModel>
	{
	}
}