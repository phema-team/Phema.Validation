namespace Phema.Validation
{
	public interface IValidationMessage
	{
		string GetMessage(object[] arguments);
	}
}