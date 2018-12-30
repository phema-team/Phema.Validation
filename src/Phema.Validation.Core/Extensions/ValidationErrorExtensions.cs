namespace Phema.Validation
{
	public static class ValidationErrorExtensions
	{
		public static void Deconstruct(this IValidationError error, out string key, out string message)
		{
			key = error.Key;
			message = error.Message;
		}

		public static void Deconstruct(this IValidationError error, out string key, out string message,
			out ValidationSeverity severity)
		{
			error.Deconstruct(out key, out message);
			severity = error.Severity;
		}
	}
}