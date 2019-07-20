namespace Phema.Validation
{
	public interface IValidationPredicate<out TValue>
	{
		IValidationContext ValidationContext { get; }
		
		string ValidationKey { get; }
		TValue Value { get; }
		
		bool? IsValid { get; set; }
	}

	public class ValidationPredicate<TValue> : IValidationPredicate<TValue>
	{
		public ValidationPredicate(
			IValidationContext validationContext,
			string validationKey,
			TValue value)
		{
			ValidationContext = validationContext;
			ValidationKey = validationKey;
			Value = value;
		}
		
		public IValidationContext ValidationContext { get; }
		public string ValidationKey { get; }
		public TValue Value { get; }
		public bool? IsValid { get; set; }
	}
}