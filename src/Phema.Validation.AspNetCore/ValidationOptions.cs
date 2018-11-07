using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public class ValidationOptions
	{
		public ValidationOptions()
		{
			Validations = new Dictionary<Type, Func<IServiceProvider, Validation>>();
		}
		
		public IDictionary<Type, Func<IServiceProvider, Validation>> Validations { get; }
	}
}