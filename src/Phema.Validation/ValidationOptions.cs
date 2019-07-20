using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public sealed class ValidationOptions
	{
		public ValidationOptions()
		{
			DefaultValidationSeverity = ValidationDefaults.DefaultValidationSeverity;
			DefaultValidationDetailsFactory = ValidationDefaults.DefaultValidationDetailsFactory;
			DefaultValidationPath = ValidationDefaults.DefaultValidationPath;
		}

		public ValidationSeverity DefaultValidationSeverity { get; set; }
		public Func<ICollection<IValidationDetail>> DefaultValidationDetailsFactory { get; set; }
		public string? DefaultValidationPath { get; set; }
	}
}