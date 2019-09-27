namespace Phema.Validation
{
	public static class ValidationDetailExtensions
	{
		public static void Deconstruct(
			this ValidationDetail validationDetail,
			out string validationKey,
			out string validationMessage)
		{
			validationKey = validationDetail.ValidationKey;
			validationMessage = validationDetail.ValidationMessage;
		}

		public static void Deconstruct(
			this ValidationDetail validationDetail,
			out string validationKey,
			out string validationMessage,
			out bool isValid)
		{
			validationKey = validationDetail.ValidationKey;
			validationMessage = validationDetail.ValidationMessage;
			isValid = validationDetail.IsValid;
		}

		public static void Deconstruct(
			this ValidationDetail validationDetail,
			out string validationKey,
			out string validationMessage,
			out bool isValid,
			out ValidationSeverity validationSeverity)
		{
			validationKey = validationDetail.ValidationKey;
			validationMessage = validationDetail.ValidationMessage;
			isValid = validationDetail.IsValid;
			validationSeverity = validationDetail.ValidationSeverity;
		}
	}
}