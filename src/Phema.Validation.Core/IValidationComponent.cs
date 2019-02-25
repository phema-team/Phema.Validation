namespace Phema.Validation
{
	/// <summary>
	/// Нетипизированный компонент, содержащий один и более <see cref="IValidationTemplate"/> для валидации
	/// </summary>
	public interface IValidationComponent
	{
	}

	/// <summary>
	/// Типизированный <see cref="TModel"/> компонент, содержащий один и более <see cref="IValidationTemplate"/> для валидации
	/// </summary>
	/// <typeparam name="TModel"></typeparam>
	public interface IValidationComponent<TModel> : IValidationComponent
	{
	}
}