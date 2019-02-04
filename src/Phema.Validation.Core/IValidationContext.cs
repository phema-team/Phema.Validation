using System;
using System.Collections.Generic;

namespace Phema.Validation
{
	/// <summary>
	/// Контекст валидации, используется для хранения текущего состояния валидации
	/// </summary>
	public interface IValidationContext : IValidationSelector, IServiceProvider
	{
		/// <summary>
		/// Задает уровень, при котором ошибка считается критичной
		/// </summary>
		ValidationSeverity Severity { get; }
		
		/// <summary>
		/// Хранит все ошибки, произошедшие с момента создания контекста
		/// </summary>
		IReadOnlyCollection<IValidationError> Errors { get; }
		
		/// <summary>
		/// Используется чтобы задать ключ ошибки и значение для валидации
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <typeparam name="TValue"></typeparam>
		/// <returns></returns>
		IValidationCondition<TValue> When<TValue>(IValidationKey key, TValue value);
	}
}