using System;

namespace Phema.Validation
{
	/// <summary>
	/// Используется для выбора сообщения, аргументов и уровня критичности
	/// </summary>
	public interface IValidationSelector
	{
		/// <summary>
		/// Метод, в котором выбираются параметры валидации
		/// </summary>
		/// <param name="selector"></param>
		/// <param name="arguments"></param>
		/// <param name="severity"></param>
		/// <returns></returns>
		IValidationError Add(Func<IServiceProvider, IValidationTemplate> selector, object[] arguments, ValidationSeverity severity);
	}
}