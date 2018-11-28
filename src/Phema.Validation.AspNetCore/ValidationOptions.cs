using System;
using System.Collections.Generic;
using System.Globalization;

namespace Phema.Validation
{
	public sealed class ValidationOptions
	{
		public ValidationOptions()
		{
			Severity = ValidationSeverity.Error;
			CultureInfo = CultureInfo.InvariantCulture;
			Validations = new Dictionary<Type, Action<IServiceProvider, object>>();
		}
		
		public ValidationSeverity Severity { get; set; }
		public CultureInfo CultureInfo { get; set; }
		
		internal IDictionary<Type, Action<IServiceProvider, object>> Validations { get; }
	}
}