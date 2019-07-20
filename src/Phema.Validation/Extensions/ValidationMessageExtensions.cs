namespace Phema.Validation
{
	public static class ValidationMessageExtensions
	{
		public static void Deconstruct(
			this IValidationDetail validationDetail,
			out string key,
			out string message)
		{
			key = validationDetail.ValidationKey;
			message = validationDetail.ValidationMessage;
		}

		public static void Deconstruct(
			this IValidationDetail validationDetail,
			out string key,
			out string message,
			out ValidationSeverity severity)
		{
			validationDetail.Deconstruct(out key, out message);
			severity = validationDetail.ValidationSeverity;
		}
	}
}