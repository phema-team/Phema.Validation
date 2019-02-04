using System;

namespace Phema.Validation
{
	/// <summary>
	/// Используется для настройки условий валидации <see cref="TValue"/>
	/// </summary>
	/// <typeparam name="TValue"></typeparam>
	public interface IValidationCondition<out TValue> : IValidationSelector
	{
		/// <summary>
		/// Метод, в которой передается <see cref="selector"/> для валидации значения <see cref="TValue"/>
		/// </summary>
		/// <param name="selector"></param>
		/// <returns></returns>
		IValidationCondition<TValue> Is(Func<TValue, bool> selector);
	}
}