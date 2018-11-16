namespace Phema.Validation
{
	public abstract class Validation
	{
		internal abstract void WhenCore(IValidationContext validationContext, object model);
	}

	public abstract class Validation<TModel> : Validation
	{
		protected abstract void When(IValidationContext validationContext, TModel model);

		internal sealed override void WhenCore(IValidationContext validationContext, object model)
		{
			When(validationContext, (TModel)model);
		}
	}
}