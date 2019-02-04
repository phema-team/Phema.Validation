namespace Phema.Validation
{
	public sealed class ExpressionPhemaValidationOptions
	{
		public ExpressionPhemaValidationOptions()
		{
			Separator = ":";
			UseDataContractPrefix = false;
		}
		
		public string Separator { get; set; }
		public bool UseDataContractPrefix { get; set; }
	}
}