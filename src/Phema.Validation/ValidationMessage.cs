namespace Phema.Validation
{
	public interface IValidationDetail
	{
		string ValidationKey { get; }
		string ValidationMessage { get; }
		ValidationSeverity ValidationSeverity { get; }
	}

	public class ValidationDetail : IValidationDetail
	{
		public ValidationDetail(string validationKey, string validationMessage, ValidationSeverity validationSeverity)
		{
			ValidationKey = validationKey;
			ValidationMessage = validationMessage;
			ValidationSeverity = validationSeverity;
		}

		public string ValidationKey { get; }
		public string ValidationMessage { get; }
		public ValidationSeverity ValidationSeverity { get; }
	}
}