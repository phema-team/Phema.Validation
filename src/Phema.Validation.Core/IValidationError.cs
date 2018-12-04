namespace Phema.Validation
{
	public interface IValidationError
	{
		string Key { get; }
		string Message { get; }
		ValidationSeverity Severity { get; }
	}
}