namespace Phema.Validation
{
	public interface IValidationError
	{
		string Key { get; }
		string Message { get; }
		ValidationSeverity Severity { get; }
	}
	
	internal class ValidationError : IValidationError
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