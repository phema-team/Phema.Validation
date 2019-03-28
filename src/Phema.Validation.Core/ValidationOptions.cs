namespace Phema.Validation
{
	public sealed class ValidationOptions
	{
		public ValidationOptions()
		{
			Severity = ValidationSeverity.Error;
		}

		public ValidationSeverity Severity { get; set; }
	}
}