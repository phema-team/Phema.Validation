namespace Phema.Validation
{
	public interface IValidationComponent
	{
	}
	
	public interface IValidationComponent<TModel> : IValidationComponent
	{
	}
	
	public interface IValidationComponent<TModel, TValidation> : IValidationComponent<TModel>
		where TValidation : class, IValidation<TModel>
	{
	}
}