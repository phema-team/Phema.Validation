namespace Phema.Validation
{
	public interface IValidation<in TModel>
	{
		void Validate(IValidationContext validationContext, TModel model);
	}
}