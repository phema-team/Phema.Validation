namespace Phema.Validation
{
	public interface IValidationMessage
	{
		string Key { get; }
		string Message { get; }
		ValidationSeverity Severity { get; }
	}

	public class ValidationMessage : IValidationMessage
	{
		public ValidationMessage(string key, string message, ValidationSeverity severity)
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