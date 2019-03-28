namespace Phema.Validation
{
	public interface IValidator<in TModel>
	{
		void Validate(IValidationContext validationContext, TModel model);
	}
}