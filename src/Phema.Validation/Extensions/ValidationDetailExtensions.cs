namespace Phema.Validation
{
	public static class ValidationDetailExtensions
	{
		public static void Deconstruct(
			this ValidationDetail validationDetail,
			out string? key,
			out string? message)
		{
			key = validationDetail?.ValidationKey;
			message = validationDetail?.ValidationMessage;
		}

		public static void Deconstruct(
			this ValidationDetail validationDetail,
			out string? key,
			out string? message,
			out ValidationSeverity validationSeverity)
		{
			validationDetail.Deconstruct(out key, out message);
			validationSeverity = validationDetail.ValidationSeverity;
		}
	}
}