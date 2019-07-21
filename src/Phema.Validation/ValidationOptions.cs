using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public sealed class ValidationOptions
	{
		public ValidationOptions()
		{
			ValidationSeverity = ValidationDefaults.DefaultValidationSeverity;
			ValidationDetailsFactory = ValidationDefaults.DefaultValidationDetailsFactory;
			ValidationPath = ValidationDefaults.DefaultValidationPath;
		}

		public ValidationSeverity ValidationSeverity { get; set; }
		public Func<ICollection<IValidationDetail>> ValidationDetailsFactory { get; set; }
		public string? ValidationPath { get; set; }
	}
}