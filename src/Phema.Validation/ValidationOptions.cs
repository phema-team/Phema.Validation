namespace Phema.Validation
{
	public sealed class ValidationOptions
	{
		public ValidationOptions()
		{
			Global = "";
			Separator = ":";
			Severity = ValidationSeverity.Error;
		}

		public string Separator { get; set; }
		public string Global { get; set; }
		public ValidationSeverity Severity { get; set; }
	}
}