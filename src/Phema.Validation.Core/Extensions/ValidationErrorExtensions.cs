namespace Phema.Validation
{
	public static class ValidationErrorExtensions
	{
		/// <summary>
		/// Метод для деконструирования <see cref="IValidationError"/> ключа и сообщения в кортеж/>
		/// </summary>
		/// <param name="error"></param>
		/// <param name="key"></param>
		/// <param name="message"></param>
		public static void Deconstruct(
			this IValidationError error,
			out string key,
			out string message)
		{
			key = error.Key;
			message = error.Message;
		}

		/// <summary>
		/// Метод для деконструирования <see cref="IValidationError"/> ключа, сообщения и уровня критичности в кортеж/>
		/// </summary>
		/// <param name="error"></param>
		/// <param name="key"></param>
		/// <param name="message"></param>
		/// <param name="severity"></param>
		public static void Deconstruct(
			this IValidationError error,
			out string key,
			out string message,
			out ValidationSeverity severity)
		{
			error.Deconstruct(out key, out message);
			severity = error.Severity;
		}
	}
}