namespace Phema.Validation
{
	/// <summary>
	/// Валидационная ошибка, происходит если не выполнено одно или несколько условий валидации
	/// </summary>
	public interface IValidationError
	{
		/// <summary>
		/// Строковый ключ валидационной ошибки, переданный в методе <see cref="IValidationContext.When{TValue}"/>
		/// </summary>
		string Key { get; }
		
		/// <summary>
		/// Сообщение с человекочитаемой ошибкой валидации, переданное в методе <see cref="IValidationSelector.Add"/>
		/// </summary>
		string Message { get; }
		
		/// <summary>
		/// Уровень критичности, с которым произошла ошибка, переданный в методе <see cref="IValidationSelector.Add"/>
		/// </summary>
		ValidationSeverity Severity { get; }
	}
}