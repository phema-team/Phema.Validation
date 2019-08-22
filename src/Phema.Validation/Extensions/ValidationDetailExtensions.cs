namespace Phema.Validation
{
	public static class ValidationDetailExtensions
	{
		public static void Deconstruct(
			this IValidationDetail validationDetail,
			out string? validationKey,
			out string? validationMessage)
		{
			validationKey = validationDetail?.ValidationKey;
			validationMessage = validationDetail?.ValidationMessage;
		}

		public static void Deconstruct(
			this IValidationDetail validationDetail,
			out string? validationKey,
			out string? validationMessage,
			out ValidationSeverity? validationSeverity)
		{
			validationKey = validationDetail?.ValidationKey;
			validationMessage = validationDetail?.ValidationMessage;
			validationSeverity = validationDetail?.ValidationSeverity;
		}
	}
}