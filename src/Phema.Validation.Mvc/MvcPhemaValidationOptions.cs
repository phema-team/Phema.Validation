using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	internal sealed class MvcPhemaValidationOptions
	{
		public MvcPhemaValidationOptions()
		{
			Dispatchers = new Dictionary<Type, Action<IValidationContext, object>>();
		}
		
		public IDictionary<Type, Action<IValidationContext, object>> Dispatchers { get; }
	}
}