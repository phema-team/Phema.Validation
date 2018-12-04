namespace Phema.Validation
{
	internal sealed class ValidationError : IValidationError
	{
		public ValidationError(string key, string message, ValidationSeverity severity)
		{
			Key = key;
			Message = message;
			Severity = severity;
		}
		
		public string Key { get; }
		public string Message { get; }
		public ValidationSeverity Severity { get; }
	}
}