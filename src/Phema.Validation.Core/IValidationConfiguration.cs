using Microsoft.Extensions.DependencyInjection;

namespace Phema.Validation
{
	/// <summary>
	/// Добавляет в контейнер компоненты валидации и шаблонов
	/// </summary>
	public interface IValidationConfiguration
	{
		IServiceCollection Services { get; }
	}
}