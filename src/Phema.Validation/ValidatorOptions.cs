using System;
using System.Collections.Generic;

namespace Phema.Validation.Internal
{
	internal sealed class ValidatorOptions
	{
		public ValidatorOptions()
		{
			Dispatchers = new Dictionary<Type, Action<IServiceProvider, IValidationContext, object>>();
		}

		public IDictionary<Type, Action<IServiceProvider, IValidationContext, object>> Dispatchers { get; }
	}
}