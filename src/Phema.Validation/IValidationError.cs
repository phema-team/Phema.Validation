namespace Phema.Validation
{
	public interface IValidationError
	{
		string Key { get; }
		string Message { get; }
	}
	
	public class ValidationError : IValidationError
	{
		public ValidationError(string key, string message)
		{
			Key = key;
			Message = message;
		}
		
		public string Key { get; }
		public string Message { get; }
	}
}