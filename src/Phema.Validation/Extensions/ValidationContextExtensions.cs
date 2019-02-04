namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		/// <inheritdoc cref="IValidationContext.When{TValue}"/>
		public static IValidationSelector When(this IValidationContext validationContext)
		{
			return validationContext.When(string.Empty);
		}

		/// <inheritdoc cref="IValidationContext.When{TValue}"/>
		public static IValidationSelector When(this IValidationContext validationContext, string key)
		{
			return validationContext.When<object>(new ValidationKey(key), null);
		}

		/// <inheritdoc cref="IValidationContext.When{TValue}"/>
		public static IValidationCondition<TValue> When<TValue>(
			this IValidationContext validationContext,
			string key,
			TValue value)
		{
			return validationContext.When(new ValidationKey(key), value);
		}
		
		/// <inheritdoc cref="ValidationContextExtensions.IsValid"/>
		public static bool IsValid(this IValidationContext validationContext, string key)
		{
			return validationContext.IsValid(new ValidationKey(key));
		}
		
		/// <inheritdoc cref="ValidationContextExtensions.EnsureIsValid"/>
		public static void EnsureIsValid(this IValidationContext validationContext, string key)
		{
			validationContext.EnsureIsValid(new ValidationKey(key));
		}
	}
}