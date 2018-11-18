namespace Phema.Validation
{
	public abstract class Validation
	{
		internal abstract void ValidateCore(IValidationContext validationContext, object model);
	}

	public abstract class Validation<TModel> : Validation
	{
		protected abstract void Validate(IValidationContext validationContext, TModel model);

		internal sealed override void ValidateCore(IValidationContext validationContext, object model)
		{
			Validate(validationContext, (TModel)model);
		}
	}
}