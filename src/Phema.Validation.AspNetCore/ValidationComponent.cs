namespace Phema.Validation
{
	public abstract class ValidationComponent
	{
		protected ValidationMessage Register(Template factory)
		{
			return new ValidationMessage(factory);
		}

		protected ValidationMessage<TValue> Register<TValue>(Template factory)
		{
			return new ValidationMessage<TValue>(factory);
		}

		protected ValidationMessage<TValue1, TValue2> Register<TValue1, TValue2>(Template factory)
		{
			return new ValidationMessage<TValue1, TValue2>(factory);
		}
	}
	
	public abstract class ValidationComponent<TModel> : ValidationComponent
	{
	}
	
	public abstract class ValidationComponent<TModel, TValidation> : ValidationComponent<TModel>
		where TValidation : Validation<TModel>
	{
	}
}