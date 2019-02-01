namespace Phema.Validation
{
	/// <summary>
	/// Валидационный шаблон с информацией об ошибке
	/// </summary>
	public interface IValidationTemplate
	{
		/// <summary>
		/// Метод для подстановки параметров в валидационный шаблон
		/// </summary>
		/// <param name="arguments"></param>
		/// <returns></returns>
		string GetMessage(object[] arguments);
	}
}