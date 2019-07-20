namespace Phema.Validation
{
	public interface IValidationCondition<out TValue>
	{
		IValidationContext ValidationContext { get; }
		
		string ValidationKey { get; }
		TValue Value { get; }
		
		bool? IsValid { get; set; }
	}

	public class ValidationCondition<TValue> : IValidationCondition<TValue>
	{
		public ValidationCondition(
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