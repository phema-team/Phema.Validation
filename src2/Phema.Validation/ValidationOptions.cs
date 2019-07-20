namespace Phema.Validation
{
	public class ValidationOptions
	{
		public ValidationOptions()
		{
			// TODO: Prefix? Options? CreateSubPrefixValidationContext?
			// DefaultPrefix = ValidationDefaults.DefaultPrefix;
			// DefaultPrefixSeparator = ValidationDefaults.DefaultPrefixSeparator;
			DefaultValidationSeverity = ValidationDefaults.DefaultValidationSeverity;
		}

		// TODO: Prefix? Options? CreateSubPrefixValidationContext?
		// public string DefaultPrefix { get; set; }
		// public string DefaultPrefixSeparator { get; set; }
		public ValidationSeverity DefaultValidationSeverity { get; set; }
	}
}