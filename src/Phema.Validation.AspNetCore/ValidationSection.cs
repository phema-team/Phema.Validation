using System;

namespace Phema.Validation
{
	public class ValidationSection
	{
		protected ValidationSection()
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