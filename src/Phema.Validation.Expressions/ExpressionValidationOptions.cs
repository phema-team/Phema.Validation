namespace Phema.Validation
{
	public class ExpressionValidationOptions
	{
		public ExpressionValidationOptions()
		{
			Separator = ":";
			UseDataContractPrefix = false;
		}
		
		public string Separator { get; set; }
		public bool UseDataContractPrefix { get; set; }
	}
}