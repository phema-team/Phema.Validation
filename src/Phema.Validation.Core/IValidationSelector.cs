using System;

namespace Phema.Validation
{
	public interface IValidationSelector
	{
		IValidationError Add(
			Func<IServiceProvider, IValidationTemplate> selector,
			object[] arguments,
			ValidationSeverity severity);
	}
}