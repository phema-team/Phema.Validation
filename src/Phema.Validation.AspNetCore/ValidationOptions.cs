using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public class ValidationOptions
	{
		public ValidationOptions()
		{
			Severity = ValidationSeverity.Error;
			Validations = new Dictionary<Type, Func<IServiceProvider, Validation>>();
		}
		
		public ValidationSeverity Severity { get; set; }
		
		internal IDictionary<Type, Func<IServiceProvider, Validation>> Validations { get; }
	}
}