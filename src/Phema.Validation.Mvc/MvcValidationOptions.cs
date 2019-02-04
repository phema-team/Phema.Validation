using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	internal class MvcValidationOptions
	{
		public MvcValidationOptions()
		{
			Dispatchers = new Dictionary<Type, Action<IValidationContext, object>>();
		}
		
		public IDictionary<Type, Action<IValidationContext, object>> Dispatchers { get; }
	}
}