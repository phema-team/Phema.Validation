namespace Phema.Validation
{
	public interface IValidationComponent<TModel, TValidation> : IValidationComponent<TModel>
		where TValidation : class, IValidator<TModel>
	{
	}
}