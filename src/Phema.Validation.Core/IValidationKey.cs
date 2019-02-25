namespace Phema.Validation
{
	/// <summary>
	/// Ключ валидации, передается в <see cref="IValidationContext.When{TValue}"/>
	/// </summary>
	public interface IValidationKey
	{
		/// <summary>
		/// Строковое значение ключа валидации
		/// </summary>
		string Key { get; }
	}
}