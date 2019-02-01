namespace Phema.Validation
{
	/// <summary>
	/// Компонент для валидации <see cref="TModel"/>
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public interface IValidation<TModel>
	{
		/// <summary>
		/// Метод, в котором должна быть описана логика валидации <see cref="TModel"/>
		/// </summary>
		/// <param name="validationContext"></param>
		/// <param name="model"></param>
		void Validate(IValidationContext validationContext, TModel model);
	}
}