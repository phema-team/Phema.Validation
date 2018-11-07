using System;

namespace Phema.Validation
{
	public class ValidationComponent<TModel, TValidation>
		where TValidation : Validation<TModel>
	{
		protected ValidationComponent()
		{
		}

		protected ValidationMessage Register(Func<string> factory)
		{
			return new ValidationMessage(factory);
		}

		protected ValidationMessage<TValue> Register<TValue>(Func<string> factory)
		{
			return new ValidationMessage<TValue>(factory);
		}

		protected ValidationMessage<TValue1, TValue2> Register<TValue1, TValue2>(Func<string> factory)
		{
			return new ValidationMessage<TValue1, TValue2>(factory);
		}
	}
}