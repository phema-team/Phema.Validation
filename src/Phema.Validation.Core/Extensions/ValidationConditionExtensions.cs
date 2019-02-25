using System;

namespace Phema.Validation
{
	public static class ValidationConditionExtensions
	{
		/// <summary>
		/// Расширение метода <see cref="IValidationCondition{TValue}.Is"/> без передаваемого значения <see cref="TValue"/>
		/// </summary>
		/// <param name="condition"></param>
		/// <param name="selector"></param>
		/// <typeparam name="TValue"></typeparam>
		/// <returns></returns>
		public static IValidationCondition<TValue> Is<TValue>(this IValidationCondition<TValue> condition, Func<bool> selector)
		{
			return condition.Is(value => selector());
		}
	}
}