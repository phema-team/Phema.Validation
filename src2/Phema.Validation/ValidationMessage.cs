namespace Phema.Validation
{
	public interface IValidationMessage
	{
		string ValidationKey { get; }
		string Message { get; }
		ValidationSeverity Severity { get; }
	}

	public class ValidationMessage : IValidationMessage
	{
		public ValidationMessage(string key, string message, ValidationSeverity severity)
		{
			ValidationKey = key;
			Message = message;
			Severity = severity;
		}

		public string ValidationKey { get; }
		public string Message { get; }
		public ValidationSeverity Severity { get; }
	}
}