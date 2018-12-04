using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public sealed class ValidationOptions
	{
		public ValidationOptions()
		{
			Severity = ValidationSeverity.Error;
			Validations = new Dictionary<Type, Action<IServiceProvider, object>>();
		}
		
		public ValidationSeverity Severity { get; set; }
		internal IDictionary<Type, Action<IServiceProvider, object>> Validations { get; }
	}
}