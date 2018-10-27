namespace Phema.Validation{
	public interface IValidationError
	{
		string Key { get; }
		string Message { get; }
	}
	
	internal class ValidationError : IValidationError
	{
		public string Key { get; set; }
		public string Message { get; set; }
	}
}