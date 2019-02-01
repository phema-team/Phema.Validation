using System.Linq;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		/// <summary>
		/// Метод, проверяющий есть ли в валидационном контексте критичные <see cref="IValidationError"/>, с возможностью проверки по <see cref="IValidationKey"/>
		/// </summary>
		/// <param name="validationContext"></param>
		/// <param name="validationKey"></param>
		/// <returns></returns>
		public static bool IsValid(this IValidationContext validationContext, IValidationKey validationKey = null)
		{
			return !validationContext.Errors
				.Where(error => error.Severity >= validationContext.Severity)
				.Any(error => validationKey == null || error.Key == validationKey.Key);
		}

		/// <summary>
		/// Метод, проверяющий есть ли в валидационном контексте критичные <see cref="IValidationError"/>, с возможностью проверки по <see cref="IValidationKey"/>
		/// Если в контексте присутствуют критичные <see cref="IValidationError"/> будет выброшен <see cref="ValidationContextException"/>
		/// </summary>
		/// <param name="validationContext"></param>
		/// <param name="validationKey"></param>
		/// <exception cref="ValidationContextException"></exception>
		public static void EnsureIsValid(this IValidationContext validationContext, IValidationKey validationKey = null)
		{
			if (!validationContext.IsValid(validationKey))
			{
				throw new ValidationContextException(validationContext.Errors, validationContext.Severity);
			}
		}
	}
}