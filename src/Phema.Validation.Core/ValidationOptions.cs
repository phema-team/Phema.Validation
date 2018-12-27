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
			Separator = ":";
		}
		
		public ValidationSeverity Severity { get; set; }

		public string Separator { get; set; }
		public IDictionary<Type, Action<IServiceProvider, object>> Validations { get; }
	}
}