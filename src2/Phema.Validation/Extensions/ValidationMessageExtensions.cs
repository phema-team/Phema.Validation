namespace Phema.Validation
{
	public static class ValidationMessageExtensions
	{
		public static void Deconstruct(
			this IValidationMessage validationMessage,
			out string key,
			out string message)
		{
			key = validationMessage.Key;
			message = validationMessage.Message;
		}

		public static void Deconstruct(
			this IValidationMessage validationMessage,
			out string key,
			out string message,
			out ValidationSeverity severity)
		{
			validationMessage.Deconstruct(out key, out message);
			severity = validationMessage.Severity;
		}
	}
}