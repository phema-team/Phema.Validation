using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	public class ValidationOptions
	{
		public ValidationOptions()
		{
			DefaultValidationSeverity = ValidationDefaults.DefaultValidationSeverity;
			DefaultValidationMessageFactory = ValidationDefaults.DefaultValidationMessagesFactory;
			DefaultValidationPath = ValidationDefaults.DefaultValidationPath;
		}

		public ValidationSeverity DefaultValidationSeverity { get; set; }
		public Func<ICollection<IValidationMessage>> DefaultValidationMessageFactory { get; set; }
		public string DefaultValidationPath { get; set; }
	}
}