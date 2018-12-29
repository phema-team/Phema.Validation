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
			Global = "";
		}
		
		public ValidationSeverity Severity { get; set; }

		public string Separator { get; set; }
		public string Global { get; set; }
		public IDictionary<Type, Action<IServiceProvider, object>> Validations { get; }
	}
}