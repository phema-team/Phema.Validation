using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	internal class ValidationComponentOptions
	{
		public ValidationComponentOptions()
		{
			Validations = new Dictionary<Type, Action<IServiceProvider, object>>();
		}
		
		public IDictionary<Type, Action<IServiceProvider, object>> Validations { get; }
	}
}