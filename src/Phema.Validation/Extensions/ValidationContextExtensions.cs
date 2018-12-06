using System.Linq;

namespace Phema.Validation
{
	public static class ValidationContextExtensions
	{
		public static IValidationCondition When(this IValidationContext validationContext)
		{
			return validationContext.When(new ValidationKey(""), (object)null);
		}
		
		public static IValidationCondition<TValue> When<TValue>(this IValidationContext validationContext, ValidationKey key, TValue value)
		{
			return validationContext.When(key, value);
		}
		
		public static bool IsValid(
			this IValidationContext validationContext, 
			ValidationKey key)
		{
			return !validationContext.SevereErrors().Any(error => error.Key == key.Key);
		}
	}
}